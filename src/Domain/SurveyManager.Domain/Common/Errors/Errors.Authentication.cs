using ErrorOr;

namespace SurveyManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error CredentialsNotValid => Error.Validation(
            code: "User.CredentialsNotValid",
            description: "Credentials are not valid! ⚠️");
    }
}