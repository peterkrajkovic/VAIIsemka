using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace VAIIsemka.Pages
{
    public class SignInModel : PageModel
    {
        private readonly ILogger<SignInModel> _logger;
        [BindProperty]
        public string? LoginAddress { get; set; }
        [BindProperty]
        public string? LoginPassword { get; set; }

        public SignInModel(ILogger<SignInModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLogin()
        {

            return Page();
        }
        public IActionResult OnPostSignUp()
        {

            return Page();
        }
    }
}