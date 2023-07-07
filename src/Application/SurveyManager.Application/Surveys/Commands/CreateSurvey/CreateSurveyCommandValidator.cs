using FluentValidation;

namespace SurveyManager.Application.Surveys.Commands.CreateSurvey;

public class CreateSurveyCommandValidator : AbstractValidator<CreateSurveyCommand>
{
    public CreateSurveyCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(5);
        RuleFor(x => x.Questions).NotEmpty();
        RuleFor(x => x.Questions.Select(x => x.Type)).NotEmpty();
        RuleFor(x => x.Questions.Select(x => x.Title)).NotEmpty();
    }
}