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
                return Task.FromResult(Functions.UserFunctions.UpdateProfile(gu, nam, usernam, dat, bio, picture));
            }
            return Task.FromResult(false);
        }

        [EnableCors]
        [HttpPost("NewPost")]
        public bool NewPost([FromForm] string guid, [FromForm] string caption, [FromForm] string location, [FromForm] IFormFile? image1, [FromForm] IFormFile? image2, [FromForm] IFormFile? image3)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            string? captio = InputHandler.SanitizeInputString(ref caption);
            string? locatio = InputHandler.SanitizeInputString(ref location);
            if (!string.IsNullOrEmpty(gui) && InputHandler.IsGuid(ref gui) && !string.IsNullOrEmpty(captio) && !string.IsNullOrEmpty(locatio))
            {
                UserFunctions.NewPost(gui, captio, locatio, image1, image2, image3);
                return true;
            }
            return false;
        }

        [EnableCors]
        [HttpGet("IsLiked")]
        public string? IsLiked(string postId, string guid)
        {
            string? id = InputHandler.SanitizeInputString(ref postId);
            string? gui = InputHandler.SanitizeInputString(ref guid);
            return UserFunctions.IsLiked(id, gui);
        }

        [EnableCors]
        [HttpGet("GetLikes")]
        public string? GetLikes(string postId)
        {
            string? id = InputHandler.SanitizeInputString(ref postId);
            return UserFunctions.GetLikes(id);
        }

        [EnableCors]
        [HttpGet("IsMe/{guid},{username}")]
        public bool IsMe(string guid, string username)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            string? usernam = InputHandler.SanitizeInputString(ref username);
            if (InputHandler.IsGuid(ref gui))
            {
                return UserFunctions.IsMe(gui, usernam);
            }
            return false;
        }

        [EnableCors]
        [HttpGet("IsFollowing/{guid},{username}")]
        public bool IsFollowing(string guid, string username)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            string? usernam = InputHandler.SanitizeInputString(ref username);
            if (InputHandler.IsGuid(ref gui))
            {
                return UserFunctions.IsFollowing(guid, username);
            }
            return false;
        }

        [EnableCors]
        [HttpGet("ChangeLike")]
        public string? ChangeLike(string postId, string guid)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            string? id = InputHandler.SanitizeInputString(ref postId);
            if (InputHandler.IsGuid(ref gui))
            {
                return UserFunctions.ChangeLike(id, gui);
            }
            return null;
        }

        [EnableCors]
        [HttpGet("ChangeFollow")]
        public string? ChangeFollow(string guid, string username)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            string? usernam = InputHandler.SanitizeInputString(ref username);
            if (InputHandler.IsGuid(ref gui))
            {
                return UserFunctions.ChangeFollow(gui, usernam);
            }
            return null;
        }


        [HttpGet("GetUser/{guid},{username}")]
        public ActionResult<User?>? GetUser(string guid, string username)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            string? user = InputHandler.SanitizeInputString(ref username);
            if (!string.IsNullOrEmpty(user) && InputHandler.IsGuid(ref gui))
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
            string? id = InputHandler.SanitizeInputString(ref postId);
            if (!string.IsNullOrEmpty(id))
            {
                return UserFunctions.DeletePost(id);
            }
            return null;
        }

        [EnableCors]
        [HttpGet("GetFollowers")]
        public string? GetFollowers(string username)
        {
            string? usernam = InputHandler.SanitizeInputString(ref username);
            if (!string.IsNullOrEmpty(usernam))
            {
                return UserFunctions.GetFollowersNumber(usernam);
            }
            return null;
        }

        [HttpGet("FollowersNumber/{username}")]
        public string? FollowersNumber(string username)
        {
            string? usernam = InputHandler.SanitizeInputString(ref username);
            if (!string.IsNullOrEmpty(usernam))
            {
                return UserFunctions.GetFollowersNumber(usernam);
            }
            return null;
        }

        [HttpGet("FollowingsNumber/{username}")]
        public string? FollowingsNumber(string username)
        {
            string? usernam = InputHandler.SanitizeInputString(ref username);
            if (!string.IsNullOrEmpty(usernam))
            {
                return UserFunctions.GetFollowingsNumber(usernam);
            }
            return null;
        }

        [HttpGet("GetPosts/{username}")]
        public ActionResult<List<Post>?>? GetPosts(string username)
        {
            string? usernam = InputHandler.SanitizeInputString(ref username);
            if (!string.IsNullOrEmpty(usernam))
            {
                return UserFunctions.GetPosts(usernam);
            }
            return null;
        }

        [HttpGet("GetImages/{guid}")]
        public ActionResult<List<Post>?>? GetImages(string guid)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            if (InputHandler.IsGuid(ref gui))
            {
                return UserFunctions.GetImages(guid);
            }
            return null;
        }

        [EnableCors]
        [HttpGet("DeleteUser")]
        public string? DeleteUser(string guid)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            if (InputHandler.IsGuid(ref gui))
            {
                return UserFunctions.DeleteUser(gui);
            }
            else
            {
                return null;
            }

        }

        [HttpGet("IsUsernameFree/{username},{guid}")]
        public bool IsUsernameFree(string username, string guid)
        {
            string? gui = InputHandler.SanitizeInputString(ref guid);
            string? usernam = InputHandler.SanitizeInputString(ref username);
            if (InputHandler.IsGuid(ref gui) && !string.IsNullOrEmpty(usernam))
            {
                return UserFunctions.IsUsernameFree(usernam, guid);
            }
            return false;
        }
    }
}
