using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace VAIIsemka.Pages
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public string? LoginAddress { get; set; }
        [BindProperty]
        public string? LoginPassword { get; set; }
        [BindProperty]
        public string? ConfirmPassword { get; set; }
        [BindProperty]
        public string? Password { get; set; }
        [BindProperty]
        public string? Email { get; set; }


        public void OnGet()
        {

        }

        public async Task<IActionResult?> OnPostLogin()
        {
            string? guid = await Calls.Login(LoginAddress, LoginPassword);
            if (!string.IsNullOrEmpty(guid))
            {
                return RedirectToPage("/Home", new { guid = guid});
            }
            TempData["MessageLogin"] = "There is no user with these credentials.";
            return null;

        }
        public async Task<IActionResult?> OnPostSignUpAsync()
        {
            string? valid = InputHandler.IsPasswordValid(Password);
            if (String.IsNullOrEmpty(valid))
            {
                if (Password == ConfirmPassword)
                {
                    string ex = await Calls.SignUpAsync(Email, Password);
                    if (String.IsNullOrEmpty(ex))
                    {
                        return RedirectToPage("/Verification");
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