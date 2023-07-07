using ErrorOr;

namespace SurveyManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class SurveyAnswers
    {
        public static Error NotFound = Error.NotFound(
            code: "SurveyAnswer.NotFound",
            description: "SurveyAnswer is not found with given Id! ⚠️");
    }
}