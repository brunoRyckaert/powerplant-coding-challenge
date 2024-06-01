using Microsoft.AspNetCore.Mvc;
using PowerPlant.Api.Data;
using PowerPlant.Domain.Logic;

namespace PowerPlant.Api.Controllers
{
    [ApiController]
    [Route("/")]
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
        public ActionResult<IEnumerable<ProductionPlanItemDto>> PostProductionPlan(PayloadDTO payloadDto)
        {
            try
            {
                var payload = payloadDto.ToPayload();
                var productionPlan = priceCalculator
                    .CalculateProductionPlan(payload)
                    .Select(pi => ProductionPlanItemDto.FromProductionPlanItem(pi))
                    .ToArray();

                return Ok(productionPlan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
