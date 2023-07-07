using FluentValidation;

namespace SurveyManager.Application.Surveys.Queries;
public class HostSurveysQueryValidator : AbstractValidator<HostSurveysQuery>
{
    public HostSurveysQueryValidator()
    {
        RuleFor(x => x.hostId)
            .NotEmpty();
    }
}