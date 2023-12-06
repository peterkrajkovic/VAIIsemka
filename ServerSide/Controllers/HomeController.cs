using Microsoft.AspNetCore.Mvc;

namespace ServerApi.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [HttpGet("{guid}")]
        public string Get(string guid)
        {

            //Database.General db = Database.General.GetInstance;
            //var result = db.CallProcedure("Search", new Dictionary<string, string> { { "id", "111111" } });
            return "swsww";
        }

        [HttpPost("{guid}")]
        public string CreatePost(string guid)
        {

            return "swsww";
        }
    }
}
