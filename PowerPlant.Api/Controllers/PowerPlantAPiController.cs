using Microsoft.AspNetCore.Mvc;
using PowerPlant.Api.Data;
using PowerPlant.Domain;

namespace PowerPlant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerPlantApiController : ControllerBase
    {
        private readonly ILogger<PowerPlantApiController> _logger;

        public PowerPlantApiController(ILogger<PowerPlantApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetLoadFulfillment")]
        public string GetLoadFulfillment()
        {
            return "Hello Powerplant";
        }

        [HttpPost("productionplan")]
        public string PostProductionPlan(PayLoadDTO payload)
        {
            return "Hello production plan";
        }
    }
}
