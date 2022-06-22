using Microsoft.AspNetCore.Mvc;

namespace BisApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public HelloWorldController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"Hello from OTel: {_environment.EnvironmentName}");
        }
    }
}
