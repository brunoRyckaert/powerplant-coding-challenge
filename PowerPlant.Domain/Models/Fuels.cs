using System.Text.Json.Serialization;

namespace PowerPlant.Domain.Models
{
    public class Fuels
    {
        [JsonPropertyName("gas(euro/MWh)")]
        public decimal GasPrice { get; set; }
        [JsonPropertyName("kerosine(euro/MWh)")]
        public decimal KerosinePrice { get; set; }
        [JsonPropertyName("co2(euro/ton)")]
        public int Co2Price { get; set; }
        [JsonPropertyName("wind(%)")]
        public int WindPercentage { get; set; }
    }
}
