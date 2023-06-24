using FluentValidation;

namespace SurveyManager.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(user => user.Firstname)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(user => user.Lastname)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress();
            
        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}