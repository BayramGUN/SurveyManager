using FluentValidation;
using SurveyManager.Application.Authentication.Queries.Login;

namespace SurveyManager.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress();
            
        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}