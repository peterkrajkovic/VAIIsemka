using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace VAIIsemka.Pages
{
    public class ErrorModel : PageModel
    {
        public string ErrorMessage { get; set; } = "Unknown error.";


        public void OnGet(string? message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ErrorMessage = message;
            }
        }
    }
}