using Microsoft.AspNetCore.Mvc;
using PowerPlant.Api.Data;
using PowerPlant.Domain;
using PowerPlant.Domain.Logic;
using PowerPlant.Domain.Models;

namespace PowerPlant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PowerPlantApiController : ControllerBase
    {
        private readonly ILogger<PowerPlantApiController> _logger;
        private PowerPlantProductionPlanner priceCalculator;

        public PowerPlantApiController(ILogger<PowerPlantApiController> logger, PowerPlantProductionPlanner priceCalculator)
        {
            _logger = logger;
            this.priceCalculator = priceCalculator;
        }

        [HttpGet("GetLoadFulfillment")]
        public string GetLoadFulfillment()
        {
            return "Hello Powerplant";
        }

        [HttpPost("productionplan")]
        public IEnumerable<ProductionPlanItem> PostProductionPlan(PayloadDTO payloadDto)
        {
            var payload = payloadDto.ToPayload();
            var productionPlan = priceCalculator.CalculateProductionPlan(payload);
            return productionPlan;
        }
    }
}
