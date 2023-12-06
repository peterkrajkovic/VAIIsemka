using ServerApi.Database;

namespace ServerApi.Database
{
    public class Procedures
    {
        public string? GetPassword(string username) {
            General db = General.GetInstance;
            Dictionary<string, string> dc = new Dictionary<string, string>
            {
                { "Username", username }
            };
            var result = db.CallProcedure("PasswordByUsername", ref dc);

            foreach (var item in result)
            {
                if (item.Passwd != null)
                {
                    return item.Passwd.ToString();
                }
            }

            // If the username is not found or no password is returned
            return null;
        }

    }
}
