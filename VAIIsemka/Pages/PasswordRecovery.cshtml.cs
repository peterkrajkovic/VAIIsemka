using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientApp.Pages
{
    public class PasswordRecoveryModel : PageModel
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string Email { get; set; }
        public IActionResult OnGet(string? email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                Email = email;
                return null;
            } else
            {
                return RedirectToPage("/Index");
            }
        }
        public async Task<IActionResult?> OnPostSubmit()
        {
            string? valid = InputHandler.IsPasswordValid(Password!);
            if (String.IsNullOrEmpty(valid))
            {
                if (Password == ConfirmPassword)
                {
                    string? guid = await Calls.PasswordRecovery(Email!, Password!);
                    if (!String.IsNullOrEmpty(guid))
                    {
                        return RedirectToPage("/Home", new { guid = guid });
                    }
                    else
                    {
                        TempData["Message"] = "Could not recover password.";
                    }
                }
                else
                {
                    TempData["Message"] = "The passwords must be the same.";
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
