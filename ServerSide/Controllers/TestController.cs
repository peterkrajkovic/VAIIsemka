using Classes;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Functions;

namespace ServerApi.Controllers
{
    [ApiController]
    [Route("api/Test")]
    public class TestController
    {
        [HttpGet("Tests")]
        public ActionResult<List<Test>> GetTests()
        {
            var tests = TestFunctions.GetTests();
            return tests;
        }
    }
}
