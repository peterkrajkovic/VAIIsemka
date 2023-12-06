using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static ClientApp.Functions.Classes;

namespace ClientApp.Pages
{
    public class SearchModel : PageModel
    {
        public List<User> Users { get; set; } = new List<User>();
        public async Task OnGetAsync()
        {
            Users.Add(new Functions.Classes.User("~/images/home.png","username"));
            Users.Add(new Functions.Classes.User("~/images/search.png", "username2"));
            //await Calls.SearchAsync("ja");
        }
    }
}
