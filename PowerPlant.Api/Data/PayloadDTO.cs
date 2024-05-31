using PowerPlant.Domain.Models;
using System.Text.Json.Serialization;

namespace PowerPlant.Api.Data
{
    public class PayloadDTO
    {
        [JsonPropertyName("load")]
        public int Load { get; set; }
        [JsonPropertyName("fuels")]
        public Fuels Fuels { get; set; }
        [JsonPropertyName("powerplants")]
        public IEnumerable<PowerPlantDTO> PowerPlants { get; set; }
    }
}
