using Classes;
using ClientApp.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientApp.Pages
{
    [BindProperties]
    public class UserModel : PageModel
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public string Guid { get; set; }
        public User? User { get; set; }
        public bool IsMe { get; set; }
        public string Followers { get; set; }
        public string Followings { get; set; }     
        public string Following { get; set; }
        public string modalId {get; set;}

        public async Task<IActionResult?> OnGet(string guid, string? username)
        {
            if (string.IsNullOrEmpty(username))
            {
                User = await Calls.GetUser(guid);
                if (User == null)
                {
                    return RedirectToPage("/Home", new { guid = guid});
                }
                IsMe = true;
                
            } else
            {
                User = await Calls.GetUser(guid, username);
                IsMe = await Calls.IsMe(guid, username);
                
                if (User == null)
                {
                    return RedirectToPage("/Index");
                }
                if (!IsMe)
                {
                    Following = Calls.IsFollowing(guid, username).Result == true ? "Unfollow": "Follow" ;
                }
                
            }
            Guid = guid;
            TempData["Guid"] = guid;
            Followers = await Calls.Followers(username);
            Followings = await Calls.Followings(username);
            var postList = await Calls.GetPosts(User.Username!);
            if (postList != null)
            {
                Posts = postList;
            }
            return null;
        }


    }
}
