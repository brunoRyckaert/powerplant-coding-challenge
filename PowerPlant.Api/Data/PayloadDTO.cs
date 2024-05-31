using PowerPlant.Domain.Models;
using System.Text.Json.Serialization;

namespace PowerPlant.Api.Data
{
    public class PayloadDTO
    {
        [JsonPropertyName("load")]
        public int Load { get; set; }
        [JsonPropertyName("fuels")]
        public Fuels Fuels { get; set; } = new Fuels();
        [JsonPropertyName("powerplants")]
        public IEnumerable<PowerPlantDTO> PowerPlants { get; set; } = [];

        public Payload ToPayload()
        {
            return
                new Payload
                {
                    Load = Load,
                    Fuels = Fuels,
                    PowerPlants = PowerPlants.Select(powerPlantDTO => powerPlantDTO.ToPowerPlant())
                };
        }
    }
}
