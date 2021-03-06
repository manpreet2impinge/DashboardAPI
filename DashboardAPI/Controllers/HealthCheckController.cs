using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DashboardAPI.Controllers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Healthy Service.");
        }

        [HttpGet("logcheck")]
        public ActionResult LogCheck()
        {
            _logger.LogDebug("Service Log Check");
            return Ok("Log Check.");
        }
    }
}
