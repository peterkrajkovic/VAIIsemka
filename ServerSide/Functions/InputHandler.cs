using Microsoft.Security.Application;
using System.Text.RegularExpressions;

namespace ServerApi.Controllers
{
    public class InputHandler
    {
        public static string? SanitizeInputString(ref string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            string output = Sanitizer.GetSafeHtmlFragment(input);
            return output;
        }
        public static bool IsGuid(ref string guid)
        {
            return Guid.TryParse(guid, out _);
        }

        public static bool IsEmail(ref string email)
        {
            // Regular expression for a basic email format validation
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }
    }
}
