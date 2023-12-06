using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Functions;

namespace ServerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignInController : ControllerBase
    {
        [HttpGet("SignIn/{address},{password}")]
        public async Task<string?> LoginAsync(string address, string password)
        {
            string add = InputHandler.SanitizeInputString(ref address);
            string pas = InputHandler.SanitizeInputString(ref password);
            if (!String.IsNullOrEmpty(add) && !String.IsNullOrEmpty(pas) && InputHandler.IsEmail(ref add))
            {
                return Functions.User.Login(add, pas);
            }
            return null;
        }
        [HttpGet("SignUp/{address},{password}")]
        public async Task<bool> SignUpAsync(string address, string password)
        {
            string add = InputHandler.SanitizeInputString(ref address);
            string pas = InputHandler.SanitizeInputString(ref password);
            if (!String.IsNullOrEmpty(add) && !String.IsNullOrEmpty(pas) && InputHandler.IsEmail(ref add))
            {
                await Functions.User.VerifyAsync(add, pas);
                return true;
            }
            return false;
        }
        [HttpGet("Verify/{code}")]
        public async Task<string?> VerifyAsync(string code)
        {
            string co = InputHandler.SanitizeInputString(ref code);
            if (!String.IsNullOrEmpty(co))
            {
                return Functions.User.VerifyCode(co);
            }
            return null;
        }
        [HttpGet("CheckGuid/{guid}")]
        public async Task<bool> CheckGuidAsync(string guid)
        {
            string gu = InputHandler.SanitizeInputString(ref guid);
            if (!String.IsNullOrEmpty(gu) && InputHandler.IsGuid(ref gu))
            {
                return Functions.User.CheckGuid(gu);
            }
            return false;
        }

        [HttpGet("LogOut/{guid}")]
        public async Task<bool> LogOutAsync(string guid)
        {
            string gu = InputHandler.SanitizeInputString(ref guid);
            if (!String.IsNullOrEmpty(gu) && InputHandler.IsGuid(ref gu))
            {
                return await Functions.User.LogOut(gu);
            }
            return false;
        }
        [HttpPost("UpdateProfile")]
        public async Task<bool> UpdateProfileAsync(string guid, string name, string username, string date, byte[]? picture)
        {
            string gu = InputHandler.SanitizeInputString(ref guid);
            string nam = InputHandler.SanitizeInputString(ref name);
            string dat = InputHandler.SanitizeInputString(ref date);
            string usernam = InputHandler.SanitizeInputString(ref username);
            if (!String.IsNullOrEmpty(gu) && InputHandler.IsGuid(ref gu) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(username))
            {
                return Functions.User.UpdateProfile(gu,nam, usernam, dat, picture);
            }
            return false;
        }
    }
}
