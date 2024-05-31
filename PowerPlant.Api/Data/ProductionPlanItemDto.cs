using PowerPlant.Api.Helpers;
using PowerPlant.Domain.Models;
using System.Text.Json.Serialization;

namespace PowerPlant.Api.Data
{
    public class ProductionPlanItemDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("p")]
        [JsonConverter<int>(typeof(DecimalPrecisionJsonConverter), parameter: 1)] // the parameter here is the precision to be used for the serialization
        public decimal Power { get; set; }

        public static ProductionPlanItemDto FromProductionPlanItem(ProductionPlanItem productionPlanItem)
        {
            return new ProductionPlanItemDto
            {
                Name = productionPlanItem.Plant.Name,
                Power = productionPlanItem.Power,
            };
        }
    }
}
