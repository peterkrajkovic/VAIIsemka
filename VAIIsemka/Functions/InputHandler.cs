using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ClientApp.Functions
{
    public class InputHandler
    {
        public static string HashPassword(string password, string salt)
        {
            byte[] saltByte = ExtractLetters(Encoding.UTF8.GetBytes(salt));
            using (var sha512 = SHA512.Create())
            {
                // Combine password and salt bytes
                byte[] combinedBytes = Encoding.UTF8.GetBytes(password).Concat(saltByte).ToArray();

                // Compute hash value
                byte[] hashBytes = sha512.ComputeHash(combinedBytes);

                // Convert hash bytes to a base64-encoded string
                string hashedPassword = Convert.ToBase64String(hashBytes);

                return hashedPassword;
            }
        }
        static byte[] ExtractLetters(byte[] inputBytes)
        {
            string inputString = Encoding.UTF8.GetString(inputBytes);

            // Use regular expression to match only letters
            string pattern = "[a-zA-Z]";
            Regex regex = new Regex(pattern);

            // Find all matches in the input string
            MatchCollection matches = regex.Matches(inputString);

            // Concatenate matched letters into a single string
            string resultString = string.Concat(matches);

            // Convert the resulting string back to a byte array
            byte[] resultBytes = Encoding.UTF8.GetBytes(resultString);

            return resultBytes;
        }


        public static string? IsPasswordValid(string password)
        {
            //Check password length
            if (String.IsNullOrEmpty(password) || password.Length < 8)
            {
                return "Password must have at least 8 characters.";
            }

            // Check if the password contains at least one uppercase letter
            if (!password.Any(char.IsUpper))
            {
                return "Password must contain an uppercase letter.";
            }

            // Check if the password contains at least one digit
            if (!password.Any(char.IsDigit))
            {
                return "Password must contain a digit.";
            }

            // If all criteria are met, the password is valid
            return null;
        }

        public static string? IsUsernameValid(string username)
        {
            //Check username length
            if (username.Length < 6)
            {
                return "Username must have at least 6 characters.";
            }

            // Check if the username meets a maximum length requirement (adjust as needed)
            if (username.Length > 20)
            {
                return "Username cannot exceed 20 characters.";
            }

            // Check if the username contains only alphanumeric characters and underscores
            if (!System.Text.RegularExpressions.Regex.IsMatch(username, "^[a-zA-Z0-9_]+$"))
            {
                return "Username can only contain letters, numbers, and underscores.";
            }

            if (!Calls.IsUsernameFree(username))
            {
                return "Username is already used";
            }
            return null;
        }

        internal static string? IsNameValid(string name)
        {
            string[] words = name.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length >= 2)
            {
                return null;
            } else
            {
                return "Name needs both surname and name.";
            }
        }
    }
}
