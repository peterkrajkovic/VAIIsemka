using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using System.Text;

namespace ServerApi.Mails
{
    public class General
    {
        static string apiKey;
        private static General instance;
        private General()
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("Mails/appsettings.Mails.json")
                    .Build();

            // Retrieve the connection string
            apiKey = (string)configuration.GetConnectionString("DefaultConnection")!.Trim();
        }

        // Public method to access the singleton instance
        public static General GetInstance
        {
            get
            {
                // Double-check locking for thread safety
                if (instance == null)
                {
                    lock (new object())
                    {
                        if (instance == null)
                        {
                            instance = new General();
                        }
                    }
                }
                return instance;
            }
        }

        internal async Task<string> Send(string recipient, string htmlBody, string subject)
        {
            Configuration.Default.AddApiKey("api-key", apiKey);
            var apiInstance = new TransactionalEmailsApi();
            var sendSmtpEmail = new SendSmtpEmail(new SendSmtpEmailSender("Spark", "spark@europe.com"), new List<SendSmtpEmailTo> { new SendSmtpEmailTo(recipient) },null,null,htmlBody,null,"Verification"); // SendSmtpEmail | Values to send a transactional email

            // Send a transactional email
            CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            return result.ToString();

        }
    }
}
