using PowerPlant.Domain.Models;
using System.Text.Json.Serialization;

namespace PowerPlant.Api.Data
{
    public class PowerPlantDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("efficiency")]
        public decimal Efficiency { get; set; }
        [JsonPropertyName("pmin")]
        public decimal Pmin { get; set; }
        [JsonPropertyName("pmax")]
        public decimal Pmax { get; set; }

        public PowerPlant.Domain.Models.PowerPlant ToPowerPlant()
        {
            var factory = new PowerPlantFactory();

            var powerPlant = factory.NewPowerPlant(Type);
            powerPlant.Name = Name;
            powerPlant.Efficiency = Efficiency;
            powerPlant.Pmin = Pmin;
            powerPlant.Pmax = Pmax;

            return powerPlant;
        }
    }
}
