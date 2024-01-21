using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientApp.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        public bool IsSigned { get; set; }
        public string Guid { get; set; }
        public async Task<IActionResult?> OnGet(string guid)
        {
            if (!string.IsNullOrEmpty(guid) && await Calls.CheckGuid(guid!))
            {
                IsSigned = true;
                Guid = guid;
            } else
            {
                IsSigned = false;
            }
            return null;
        }

        public IActionResult OnGetLogOut()
        {
            return RedirectToPage("/Index");
        }
    }
}
