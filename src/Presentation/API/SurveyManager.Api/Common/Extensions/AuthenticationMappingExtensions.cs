using MapsterMapper;
using SurveyManager.Application.Authentication.Commands.Register;
using SurveyManager.Application.Authentication.Common;
using SurveyManager.Application.Authentication.Queries.Login;
using SurveyManager.Contracts.Authentication;

namespace SurveyManager.Api.Common.Extensions;

public static class SurveyManagerExtensions
{
    public static TCommand RequestToCommand<TCommand>(this object source, IMapper mapper) 
        => mapper.Map<TCommand>(source);
    public static LoginQuery LoginRequestToQuery(this LoginRequest source, IMapper mapper) 
        => mapper.Map<LoginQuery>(source);
    public static AuthenticationResponse AuthenticationResultToResponse(this AuthenticationResult source, IMapper mapper) 
        => mapper.Map<AuthenticationResponse>(source);
    
}