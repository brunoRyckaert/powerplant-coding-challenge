using System.Text.Json.Serialization;

namespace PowerPlant.Domain.Models
{
    public class ProductionPlanItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("p")]
        public decimal Power { get; set; }
    }
}
