using System.Security.Cryptography;
using System.Text;

namespace SurveyManager.Application.Authentication.Extensions;

public static class PasswordHashExtansion
{
    public static string GetPasswordHash(this string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
    public static bool VerifyPassword(this string password, string hashedPassword)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            string hashedInput = Convert.ToBase64String(hashedBytes);
            return hashedInput.Equals(hashedPassword);
        }
    }
}