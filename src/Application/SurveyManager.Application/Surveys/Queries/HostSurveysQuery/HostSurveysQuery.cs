using ErrorOr;
using MediatR;
using SurveyManager.Application.Surveys.Common;

namespace SurveyManager.Application.Surveys.Queries;

public record HostSurveysQuery(
    Guid hostId
) : IRequest<ErrorOr<HostSurveysResult>>;
