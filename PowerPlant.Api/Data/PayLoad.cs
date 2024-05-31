using PowerPlant.Domain;
using System.Text.Json.Serialization;

namespace PowerPlant.Api.Data
{
    public class PayLoadDTO
    {
        [JsonPropertyName("load")]
        public int Load { get; set; }
        [JsonPropertyName("fuels")]
        public Fuels Fuels { get; set; }
        [JsonPropertyName("powerplants")]
        public IEnumerable<PowerPlantDTO> PowerPlants { get; set; }
    }
}
