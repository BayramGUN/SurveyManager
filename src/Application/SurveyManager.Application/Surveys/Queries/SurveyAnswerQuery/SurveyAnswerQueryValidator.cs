using FluentValidation;

namespace SurveyManager.Application.Surveys.Queries;
public class SurveyAnswerQueryValidator : AbstractValidator<SurveyAnswerQuery>
{
    public SurveyAnswerQueryValidator()
    {
        RuleFor(survey => survey.surveyId)
            .NotEmpty();
    }
}