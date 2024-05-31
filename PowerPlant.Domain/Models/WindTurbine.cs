using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlant.Domain.Models
{
    public class WindTurbine : PowerPlant
    {
        public override decimal CostPerMwh(Fuels fuels)
        {
            return 0;
        }

        public override decimal MaxOutput(Fuels fuels)
        {
            return this.Pmax * fuels.WindPercentage / 100;
        }
    }
}
