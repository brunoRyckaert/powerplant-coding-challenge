using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PowerPlant.Domain.Models
{
    public abstract class PowerPlant
    {
        public string Name { get; set; }
        public decimal Efficiency { get; set; }
        public decimal Pmin { get; set; }
        public decimal Pmax { get; set; }

        public abstract decimal MaxOutput(Fuels fuels);
        public abstract decimal CostPerMwh(Fuels fuels);
    }
}
