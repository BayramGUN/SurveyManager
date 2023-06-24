using System.Reflection;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SurveyManager.Application.Authentication.Commands.Register;
using SurveyManager.Application.Authentication.Common;
using SurveyManager.Application.Common.Behaviors;

namespace SurveyManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidateBehavior<,>));
       
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}