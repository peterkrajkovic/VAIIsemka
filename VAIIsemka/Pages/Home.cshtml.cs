using ClientApp.Functions;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ClientApp.Functions.Classes;

namespace VAIIsemka.Pages
{
    public class HomeModel : PageModel
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public async Task<IActionResult> OnGet(string guid)
        {
            if (!String.IsNullOrEmpty(guid) && await Calls.CheckGuid(guid))
            {
                Post post = new Post("caption", "images/imag.png", "username");
                for (int i = 0; i < 4; i++)
                {
                    Posts.Add(post);
                }
                return Page();
            } else
            {
                return RedirectToPage("/Error", new {message = "You need to login to access this site." });
            }

        }
    }
}
