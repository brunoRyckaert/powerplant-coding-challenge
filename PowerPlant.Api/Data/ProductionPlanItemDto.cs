using PowerPlant.Domain.Models;
using System.Text.Json.Serialization;

namespace PowerPlant.Api.Data
{
    public class ProductionPlanItemDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("p")]
        [JsonConverter(typeof(NumericPrecisionJsonConverter))]
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
