
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SurveyManager.Api.Common.Errors;
using SurveyManager.Api.Common.Mapping;

namespace SurveyManager.Api;

public static class DependencyInjection
{
    public const string MyAllowSpecificOrigins  = "_mySpecs";

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, SurveyManagerProblemDetailsFactory>();
        services.AddMappings();
        services.AddCors(options =>
        {
        options.AddPolicy(MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                              });
        });
        return services;
    }
}