using Microsoft.AspNetCore.Mvc;
using PowerPlant.Api.Data;
using PowerPlant.Domain;
using PowerPlant.Domain.Logic;
using PowerPlant.Domain.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public IEnumerable<ProductionPlanItemDto> PostProductionPlan(PayloadDTO payloadDto)
        {
            var payload = payloadDto.ToPayload();
            var productionPlan = priceCalculator
                .CalculateProductionPlan(payload)
                .Select(pi => ProductionPlanItemDto.FromProductionPlanItem(pi))
                .ToArray();

            return productionPlan;
        }
    }
}
