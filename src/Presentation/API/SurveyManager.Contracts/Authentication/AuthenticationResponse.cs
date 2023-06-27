namespace SurveyManager.Contracts.Authentication;

public record AuthenticationResponse(
    string Id,
    string Firstname,
    string Lastname,
    string Email,
    string Token);