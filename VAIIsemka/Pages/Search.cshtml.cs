using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ClientApp.Functions.Classes;

namespace ClientApp.Pages
{
    public class SearchModel : PageModel
    {
        public List<User> Users { get; set; } = new List<User>();
        public void OnGet()
        {
        }
    }
}
