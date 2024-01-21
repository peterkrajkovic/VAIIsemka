using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VAIIsemka.Pages
{
    public class VerificationModel : PageModel
    {
        public string Email { get; set; }
        public string? Page { get; set; }
        public void OnGet(string email, string? page)
        {
            Email = email;
            Page = page;
        }


        public async Task<IActionResult?> OnPostVerify()
        {
            string input = string.Empty;
            for (int i = 1; i < 7; i++)
            {
                string inputName = $"verification{i}";
                input  += Request.Form[inputName];
            }
            var guid = await Calls.Verify(input);
            if (string.IsNullOrEmpty(guid))
            {
                TempData["Message"] = "Wrong verification code.";
                return null;
            }

            if (!String.IsNullOrEmpty(Page))
            {
                return RedirectToPage("/" + Page, new { guid = guid });
            }
            else
            {
                return RedirectToPage("/Profile", new { guid = guid, firstTime = true });
            }
        }

    }
}
