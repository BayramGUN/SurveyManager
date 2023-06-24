using Mapster;
using SurveyManager.Application.Authentication.Commands.Register;
using SurveyManager.Application.Authentication.Common;
using SurveyManager.Application.Authentication.Queries.Login;
using SurveyManager.Contracts.Authentication;

namespace SurveyManager.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
    }
}

