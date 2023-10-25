using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ClientApp.Functions.Classes;

namespace VAIIsemka.Pages
{
    public class HomeModel : PageModel
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public void OnGet()
        {
            Post post = new Post("caption", "images/imag.png", "admin");
            for (int i = 0; i < 4; i++) {
                Posts.Add(post);
            }

        }
    }
}
