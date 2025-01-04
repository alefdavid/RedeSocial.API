using Microsoft.AspNetCore.Mvc;

namespace RedeSocial.API.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Let´s go!";
        }
    }

}