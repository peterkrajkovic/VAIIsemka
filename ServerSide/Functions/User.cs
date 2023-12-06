using ServerApi.Database;
using ServerApi.Mails;
using System;
using System.Net;

namespace ServerApi.Functions
{
    public class User
    {

        public static async Task VerifyAsync(string email, string password)
        {
            Database.General db = Database.General.GetInstance;
            string code = GenerateRandomCode().ToString("D6");
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "email", email },
                { "password", password },
                { "code", code }
            };
            db.CallProcedure("dbo.Verify", ref dic);
            try
            {
                await ServerApi.Mails.General.Send(email, Templates.SignInTemplate(code), "Verification");
            } catch (Exception ex)
            {
            }
        }
        public static string? VerifyCode(string code)
        {
            Guid myGuid = Guid.NewGuid();
            string guidString = myGuid.ToString();
            Database.General db = Database.General.GetInstance;
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "code", code },
                {"guid", guidString}
            };
            var result = db.CallProcedure("dbo.VerifyCode", ref dic);
            if (result.Count > 0)
            {
                return guidString;
            }
            return null;
        }

        internal static bool CheckGuid(string guid)
        {
            Database.General db = Database.General.GetInstance;
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"guid", guid}
            };
            var result = db.CallProcedure("dbo.CheckGuid", ref dic);
            if (result.Count > 0)
            {
                return true;
            }
            return false;
        }

        internal static string? Login(string address, string password)
        {
            Database.General db = Database.General.GetInstance;
            Guid myGuid = Guid.NewGuid();
            string guidString = myGuid.ToString();
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"address", address},
                {"passwd", password},
                {"guid", guidString }
            };
            var result = db.CallProcedure("dbo.Login", ref dic);
            if (result.Count > 0)
            {
                return guidString;
            }
            return null;
        }

        internal static async Task<bool> LogOut(string gu)
        {
            throw new NotImplementedException();
        }

        internal static bool UpdateProfile(string guid, string name, string username, string date, byte[]? picture)
        {
            Database.General db = Database.General.GetInstance;
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                {"guid", guid},
                {"name", name},
                {"username",username},
                {"date", date },
                { "date" , date}
            };
            var result = db.CallProcedure("dbo.UpdateProfile", ref dic);
            return result.Count > 0;
        }

        static int GenerateRandomCode()
        {
            Random random = new Random();
            int code = random.Next(100000, 1000000); 
            return code; 
        }
    }
}
