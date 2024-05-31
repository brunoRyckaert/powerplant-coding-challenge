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

        [HttpPost("productionplan")]
        public object PostProductionPlan(PayloadDTO payloadDto)
        {
            var payload = payloadDto.ToPayload();
            var productionPlan = priceCalculator
                .CalculateProductionPlan(payload)
                .Select(pi => new
                {
                    name = pi.Plant.Name,
                    p = pi.Power
                })
                .ToArray();
            return productionPlan;
        }
    }
}
