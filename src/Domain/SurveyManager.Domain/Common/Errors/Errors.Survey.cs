using ErrorOr;

namespace SurveyManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Survey
    {
        public static Error NotFound = Error.NotFound(
            code: "Survey.NotFound",
            description: "Survey is not found with given Id! ⚠️");
    }
}