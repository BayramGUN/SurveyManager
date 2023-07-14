using FluentValidation;

namespace SurveyManager.Application.Surveys.Commands.UpdateSurvey;

public class UpdateSurveyCommandValidator : AbstractValidator<UpdateSurveyCommand>
{
    public UpdateSurveyCommandValidator()
    {
        /* RuleFor(x => x.IsActive)
            .NotEmpty(); */
        RuleFor(x => x.SurveyId).NotEmpty();
    }
}