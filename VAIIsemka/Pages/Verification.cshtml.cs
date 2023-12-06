using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VAIIsemka.Pages
{
    public class VerificationModel : PageModel
    {
        public void OnGet()
        {
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
            return RedirectToPage("/Profile", new { guid = guid, firstTime = true });
        }

        public async Task<IActionResult?> OnPostResend()
        {
            return null;
        }
    }
}
