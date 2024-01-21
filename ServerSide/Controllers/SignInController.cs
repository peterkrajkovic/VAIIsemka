using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Database;
using ServerApi.Functions;
using System;

namespace ServerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignInController : ControllerBase
    {
        [HttpGet("SignIn/{address},{password}")]
        public async Task<string?> LoginAsync(string address, string password)
        {
            string? add = InputHandler.SanitizeInputString(ref address);
            string? pas = InputHandler.SanitizeInputString(ref password);
            if (!String.IsNullOrEmpty(add) && !String.IsNullOrEmpty(pas) && InputHandler.IsEmail(ref add))
            {
                return Functions.SignInFunctions.Login(add, pas);
            }
            return null;
        }
        [HttpGet("SignUp/{address},{password}")]
        public async Task<string?> SignUpAsync(string address, string password)
        {
            string? add = InputHandler.SanitizeInputString(ref address);
            string? pas = InputHandler.SanitizeInputString(ref password);
            if (!String.IsNullOrEmpty(add) && !String.IsNullOrEmpty(pas) && InputHandler.IsEmail(ref add))
            {
                return await Functions.SignInFunctions.SignUpAsync(add, pas);
            }
            return null;
        }

        [EnableCors]
        [HttpGet("Resend/{email}")]
        public async Task<bool> ResendAsync(string email)
        {
            string? mail = InputHandler.SanitizeInputString(ref email);
            if (!String.IsNullOrEmpty(mail) && InputHandler.IsEmail(ref mail))
            {
                await Functions.UserFunctions.Resend(mail);
                return true;
            }
            return false;
        }
        [HttpGet("Verify/{code}")]
        public async Task<string?> VerifyAsync(string code)
        {
            string? co = InputHandler.SanitizeInputString(ref code);
            if (!String.IsNullOrEmpty(co))
            {
                return Functions.SignInFunctions.VerifyCode(co);
            }
            return null;
        }

        [HttpGet("CheckGuid/{guid}")]
        public async Task<bool> CheckGuidAsync(string guid)
        {
            string? gu = InputHandler.SanitizeInputString(ref guid);
            if (!String.IsNullOrEmpty(gu) && InputHandler.IsGuid(ref gu))
            {
                return true;
            }
            return false;
        }

        [EnableCors]
        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            var mail = InputHandler.SanitizeInputString(ref email);
            if (InputHandler.IsEmail(ref email))
            {
                SignInFunctions.SendForgotPasswordMailAsync(mail);
                return new JsonResult("Email sent successfully! Please, check your email.");
            }
            return new JsonResult("Email not sent.");

        }

        [HttpGet("PasswordRecovery/{email},{password}")]
        public async Task<string?> PasswordRecovery(string email, string password)
        {
            var mail = InputHandler.SanitizeInputString(ref email);
            var passwd = InputHandler.SanitizeInputString(ref password);
            if (InputHandler.IsEmail(ref email))
            {
                return SignInFunctions.PasswordRecovery(mail, passwd);
            }
            return null;

        }
    }
}
