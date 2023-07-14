using System.Text;

using Hangfire;
using Hangfire.Storage.SQLite;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Application.Common.Interfaces.Services;
using SurveyManager.Application.Common.Services;
using SurveyManager.Application.Common.Services.Authentication;
using SurveyManager.Infrastructure.Authentication;
using SurveyManager.Infrastructure.Persistence;
using SurveyManager.Infrastructure.Persistence.Interceptors;
using SurveyManager.Infrastructure.Persistence.Repositories;
using SurveyManager.Infrastructure.Services;

namespace SurveyManager.Infrastructure;

public static class DependencyInjection
{ 
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration)
                .AddPersistence(configuration)
                .AddHangfire(configuration);

        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IServiceManagement, ServiceManagement>();
        return services;
    }
    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionStringForDBContext = configuration.GetConnectionString("SQLServer");
        services.AddDbContext<SurveyManagerDbContext>(options =>
            options.UseSqlServer(connectionStringForDBContext)
        );
        
        services.AddScoped<PublishDomainEventsInterceptor>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISurveyRepository, SurveyRepository>();
        services.AddScoped<ISurveyAnswerRepository, SurveyAnswerRepository>();
        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
            
        return services;
    }
    public static IServiceCollection AddHangfire(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var connectionStringForHangfire = configuration.GetConnectionString("SQLServerForHF");
            
            services.AddHangfire(config =>
            {
                // Use SQL Server storage for Hangfire
                config.UseSQLiteStorage(connectionStringForHangfire);
            });

            // Add the Hangfire background job server
            services.AddHangfireServer();

            return services;
        }
}