using sib_api_v3_sdk.Model;

namespace ServerApi.Mails
{
    public class Templates
    {
        public static string SignInTemplate(string code)
        {
            return $@"
                    <html>
                        <head>
                            <title>Welcome to Our Community</title>
                        </head>
                        <body>
                            <h2>Hello from Spark!</h2>
                            <p>Congratulations on creating your profile with us. We are thrilled to have you as a member of our community.</p>
                            <p style='font-size: 24px; line-height: 36px;'>Your unique verification code is: <strong>{code:D6}</strong></p>
                            <p>Thank you for choosing us. We look forward to seeing you around!</p>
                        </body>
                    </html>";
        }

        public static string RecoverPasswordTemplate(string email)
        {
            return $@"
                    <html>
                        <head>
                            <title>Password recovery</title>
                        </head>
                        <body>
                            <h2>Hello from Spark!</h2>
                            <p>We received a request to reset your password. To proceed, click the link below:</p>
                            <p><a href='{"https://localhost:44310/PasswordRecovery/?email=" + email}'>Reset Password</a></p>
                            <p>If you didn't request a password reset, you can ignore this email.</p>
                            <p>Thank you!</p>
                        </body>
                    </html>";
        }
    }
}
