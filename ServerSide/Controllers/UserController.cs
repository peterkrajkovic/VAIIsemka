using Classes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Database;
using ServerApi.Functions;

namespace ServerApi.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        [HttpPost("UpdateProfile")]
        public Task<bool> UpdateProfileAsync([FromForm] string guid, [FromForm] string name, [FromForm] string username, [FromForm] string date, [FromForm] string bio, [FromForm] byte[]? picture)
        {
            string? gu = InputHandler.SanitizeInputString(ref guid);
            string? nam = InputHandler.SanitizeInputString(ref name);
            string? dat = InputHandler.SanitizeInputString(ref date);
            string? bi = InputHandler.SanitizeInputString(ref bio);
            string? usernam = InputHandler.SanitizeInputString(ref username);
            if (!String.IsNullOrEmpty(gu) && InputHandler.IsGuid(ref gu) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(username))
            {
                return Task.FromResult(Functions.UserFunctions.UpdateProfile(gu, nam, usernam, dat,bio, picture));
            }
            return Task.FromResult(false);
        }

        [EnableCors]
        [HttpPost("NewPost")]
        public bool NewPost([FromForm] string guid, [FromForm] string caption, [FromForm] string location, [FromForm] IFormFile? image1, [FromForm] IFormFile? image2, [FromForm] IFormFile? image3)
        {
            UserFunctions.NewPost(guid, caption, location, image1, image2, image3);
            return true;
        }

        [EnableCors]
        [HttpGet("IsLiked")]
        public string? IsLiked(string postId, string guid)
        {
            return UserFunctions.IsLiked(postId, guid);
        }

        [EnableCors]
        [HttpGet("GetLikes")]
        public string? GetLikes(string postId)
        {
            return UserFunctions.GetLikes(postId);
        }

        [EnableCors]
        [HttpGet("IsMe/{guid},{username}")]
        public bool IsMe(string guid, string username)
        {
            return UserFunctions.IsMe(guid, username);
        }

        [EnableCors]
        [HttpGet("IsFollowing/{guid},{username}")]
        public bool IsFollowing(string guid, string username)
        {
            return UserFunctions.IsFollowing(guid, username);
        }

        [EnableCors]
        [HttpGet("ChangeLike")]
        public string? ChangeLike(string postId, string guid)
        {
            return UserFunctions.ChangeLike(postId, guid);
        }

        [EnableCors]
        [HttpGet("ChangeFollow")]
        public string? ChangeFollow(string guid, string username)
        {
            return UserFunctions.ChangeFollow(guid, username);
        }


        [HttpGet("GetUser/{guid},{username}")]
        public ActionResult<User?>? GetUser(string guid, string username)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            string? user = InputHandler.SanitizeInputString(ref username);
            if (!string.IsNullOrEmpty(user))
            {
                return UserFunctions.GetUser(gui, user);
            }
            return null;
        }
        
        [HttpGet("GetUser/{guid}")]
        public ActionResult<User?>? GetUser(string guid)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            if (!string.IsNullOrEmpty(gui))
            {
                return UserFunctions.GetUser(gui);
            }
            return null;
        }

        [EnableCors]
        [HttpGet("DeletePost")]
        public string? DeletePost(string postId)
        {
            return UserFunctions.DeletePost(postId);
        }
        [EnableCors]
        [HttpGet("GetFollowers")]
        public string? GetFollowers(string username)
        {
            return UserFunctions.GetFollowersNumber(username);
        }

        [HttpGet("FollowersNumber/{username}")]
        public string? FollowersNumber(string username)
        {
            return UserFunctions.GetFollowersNumber(username);
        }
        [HttpGet("FollowingsNumber/{username}")]
        public string? FollowingsNumber(string username)
        {
            return UserFunctions.GetFollowingsNumber(username);
        }

        [HttpGet("GetPosts/{username}")]
        public ActionResult<List<Post>?> GetPosts(string username)
        {
            return UserFunctions.GetPosts(username);
        }

        [HttpGet("GetImages/{guid}")]
        public ActionResult<List<Post>?> GetImages(string guid)
        {
            return UserFunctions.GetImages(guid);
        }

        [EnableCors]
        [HttpGet("DeleteUser")]
        public string? DeleteUser(string guid)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            if (InputHandler.IsGuid(ref gui))
            {
                return UserFunctions.DeleteUser(gui);
            } else
            {
                return null;
            }

        }


        [HttpGet("IsUsernameFree/{username},{guid}")]
        public bool IsUsernameFree(string username, string guid)
        {
            return UserFunctions.IsUsernameFree(username, guid);
        }
    }
}
