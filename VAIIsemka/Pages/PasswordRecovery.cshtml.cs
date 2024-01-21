using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientApp.Pages
{
    [BindProperties]
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
                TempData["Email"] = Email;
                return null;
            } else
            {
                return RedirectToPage("/Index");
            }
        }
        public async Task<IActionResult?> OnPostSubmit()
        {
            if (Email == null)
            {
                Email = TempData["Email"].ToString();
            }

            string? valid = InputHandler.IsPasswordValid(Password!);
            if (String.IsNullOrEmpty(valid))
            {
                if (Password == ConfirmPassword)
                {
                    string? guid = await Calls.PasswordRecovery(Email!, InputHandler.HashPassword(Password!, Email));
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
            TempData["Email"] = Email;
            return null;
        }
    }
}
