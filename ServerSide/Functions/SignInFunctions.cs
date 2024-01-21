using Classes;
using Microsoft.EntityFrameworkCore;
using ServerApi.Database;
using ServerApi.Mails;
using System.Runtime.InteropServices.Marshalling;
using System.Xml.Serialization;
namespace ServerApi.Functions
{
    public class SignInFunctions
    {
        // DbSet properties for entities: Verification, User, Process
        public static string VerifyCode(string code)
        {
            using (DataContext db = new DataContext())
            {
                var verificationRecord = db.Verification
                    .Where(v => v.Code == code)
                    .FirstOrDefault();

                if (verificationRecord != null)
                {
                    var email = verificationRecord.Email;
                    var password = verificationRecord.Password;

                    // Delete from Verification table
                    db.Verification.Remove(verificationRecord);

                    // Insert into User table
                    db.User.Add(new User
                    {
                        Password = password,
                        Email = email
                    });
                    db.SaveChanges();
                    var us = db.User.ToList();
                    var user = db.User.Where(x => x.Email == email).FirstOrDefault();
                    if (user != null)
                    {
                        int id = user.Id;
                        string guid = GeneralFunctions.UpdateGuid(db, id);

                        // Save changes to the database
                        db.SaveChanges();

                        return guid;
                    } else
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        public static async Task SendForgotPasswordMailAsync(string email)
        {
            try
            {
                Mails.General general = Mails.General.GetInstance;
                await general.Send(email, Templates.RecoverPasswordTemplate(email), "Password Recovery");
            }
            catch (Exception ex)
            {
            }
        }

        public static async Task<string?> SignUpAsync(string email, string password)
        {
            using (DataContext db = new DataContext())
            {
                if (db.User.Where(x => x.Email == email).Count() > 0)
                {
                    return ("This email address is already in use.");
                }
                string code = GeneralFunctions.GenerateRandomCode().ToString("D6");

                try
                {
                    // Insert into Verification table
                    db.Verification.Add(new Verification
                    {
                        Email = email,
                        Code = code,
                        Password = password
                    });

                    // Save changes to the database
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Log or print the exception details
                    Console.WriteLine(ex.Message);
                    return ex.Message;
                }
                try
                {
                    Mails.General general = Mails.General.GetInstance;
                    await general.Send(email, Templates.SignInTemplate(code), "Verification");
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                return null;
            }

        }
        public static string? Login(string email, string password)
        {
            using (DataContext db = new DataContext())
            {
                var userRecord = db.User
                  .Where(v => v.Password == password && (v.Email == email || v.Username == email))
                  .FirstOrDefault();

                if (userRecord != null)
                {
                    return GeneralFunctions.UpdateGuid(db, userRecord.Id);
                }

                return null;
            }
        }
        public static string? GetCode(string email)
        {
            using (var context = new DataContext())
            {
                var data = context.Verification.FirstOrDefault(u => u.Email == email);

                if (data != null)
                {
                    return data.Code;
                }
                else
                {
                    return null;
                }
            }
        }

        public static string? PasswordRecovery(string email, string password)
        {
            using (var context = new DataContext())
            {
                var user = context.User.Where(x => x.Email == email).FirstOrDefault();

                if (user != null)
                {
                    user.Password = password;
                    context.SaveChanges();

                    return GeneralFunctions.UpdateGuid(context, user.Id);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
