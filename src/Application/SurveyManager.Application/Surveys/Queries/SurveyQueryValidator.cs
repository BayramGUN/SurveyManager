using FluentValidation;
using SurveyManager.Application.Surveys.Queries;

namespace SurveyManager.Application.Surveys.Queries;
public class SurveyQueryValidator : AbstractValidator<SurveyQuery>
{
    public SurveyQueryValidator()
    {
        RuleFor(survey => survey.Id)
            .NotEmpty();
    }
}