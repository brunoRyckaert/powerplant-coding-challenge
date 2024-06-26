﻿using System.Text.Json.Serialization;

namespace PowerPlant.Domain.Models
{
    public class Payload
    {
        [JsonPropertyName("load")]
        public int Load { get; set; }
        [JsonPropertyName("fuels")]
        public Fuels Fuels { get; set; }
        [JsonPropertyName("powerplants")]
        public IEnumerable<PowerPlant> PowerPlants { get; set; }
    }
}
