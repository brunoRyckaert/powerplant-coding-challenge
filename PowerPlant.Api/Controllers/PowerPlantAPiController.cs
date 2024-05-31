using Microsoft.AspNetCore.Mvc;
using PowerPlant.Api.Data;
using PowerPlant.Domain;
using PowerPlant.Domain.Models;

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
        public IEnumerable<ProductionPlanItem> PostProductionPlan(PayloadDTO payloadDto)
        {
            var payload = payloadDto.ToPayload();
            var productionPlan = new List<ProductionPlanItem> { new ProductionPlanItem { Name = "windpark1", Power = 90.0M }, new ProductionPlanItem { Name = "gasfiredbig1", Power = 460.0M } };
            return productionPlan;
        }
    }
}
