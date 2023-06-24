using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SurveyManager.Application.Common.Interfaces.Persistence;
using SurveyManager.Application.Common.Interfaces.Services;
using SurveyManager.Application.Common.Services.Authentication;
using SurveyManager.Infrastructure.Authentication;
using SurveyManager.Infrastructure.Persistence;
using SurveyManager.Infrastructure.Services;

namespace SurveyManager.Infrastructure;

public static class DependencyInjection
{ 
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}