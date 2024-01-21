using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VAIIsemka.Pages
{
    [BindProperties]
    public class SignInModel : PageModel
    {
        public string? LoginAddress { get; set; }
        public string? LoginPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }


        public void OnGet()
        {

        }

        public async Task<IActionResult?> OnPostLogin()
        {
            string? guid = await Calls.Login(LoginAddress!, InputHandler.HashPassword(LoginPassword!,LoginAddress!));
            if (!string.IsNullOrEmpty(guid))
            {
                return RedirectToPage("/Home", new { guid = guid});
            }
            TempData["MessageLogin"] = "There is no user with these credentials.";
            return null;

        }
        public async Task<IActionResult?> OnPostSignUp()
        {
            string? valid = InputHandler.IsPasswordValid(Password!);
            valid += InputHandler.IsEmail(Email!);
            if (String.IsNullOrEmpty(valid))
            {
                if (Password == ConfirmPassword)
                {
                    string? ex = await Calls.SignUpAsync(Email!, InputHandler.HashPassword(Password!, Email!));
                    if (String.IsNullOrEmpty(ex))
                    {
                        return RedirectToPage("/Verification", new { email = Email });
                    }
                    else
                    {
                        TempData["MessageSignUp"] = ex;
                    }
                } else
                {
                    TempData["MessageSignUp"] = "The passwords must be the same.";
                }
            }
            else
            {
                TempData["MessageSignUp"] = valid;
            }
            TempData["ShowSignupForm"] = true;
            return null;
        }
    }
}