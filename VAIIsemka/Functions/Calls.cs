using static ClientApp.Functions.Classes;

namespace ClientApp.Functions
{
    public class Calls
    {
        public static async Task<string?> Login(string address, string password)
        {
            return await Request.Login(address, password);
        }

        public static async Task<bool> LogOutAsync(string guid)
        {
            return await Request.Logout(guid);
        }

        public static async Task<string?> SignUpAsync(string address, string password)
        {
            await Request.SignUpAsync(address, password);
            return null;
        }

        public static async Task<List<Post>> SearchAsync(string id)
        {
            Request  req = new Request();
            return null; 
        }

        public static bool IsUsernameFree(string username)
        {
            return true;
        }
        public static async Task<string?> Verify(string code)
        {
            return await Request.Verify(code);
        }

        internal static async Task<bool> CheckGuid(string guid)
        {
            return await Request.CheckGuid(guid);
        }

        internal static async Task<bool> UpdateProfile(string guid, string name, string username, DateTime date, string country, byte[]? picture)
        {
            return await Request.UpdateProfile(guid, name, username, date, country, picture);
        }
    }
}
