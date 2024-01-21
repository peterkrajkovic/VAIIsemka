using Classes;
using ClientApp.Functions;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Net.Mime.MediaTypeNames;

namespace VAIIsemka.Pages
{
    [BindProperties]
    public class HomeModel : PageModel
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public string Guid { get; set; }
        public async Task<IActionResult?> OnGet(string guid)
        {
            if (!String.IsNullOrEmpty(guid) && await Calls.CheckGuid(guid))
            {
                Guid = guid;
                Posts = await Calls.GetImages(guid); 
                return null;
            } else
            {
                return RedirectToPage("/SignIn");
            }

        }
    }
}
