using FluentValidation;

namespace SurveyManager.Application.SurveyAnswers.Queries.SurveyAnswersQuery;
public class SurveyAnswersQueryValidator : AbstractValidator<SurveyAnswersQuery>
{
    public SurveyAnswersQueryValidator()
    {
        RuleFor(survey => survey.surveyId)
            .NotEmpty();
    }
}