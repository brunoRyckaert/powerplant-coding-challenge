using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlant.Domain.Models
{
    public class GasFired : PowerPlant
    {
        public override decimal CostPerMwh(Fuels fuels)
        {
            return fuels.GasPrice / this.Efficiency;
        }

        public override decimal MaxOutput(Fuels fuels)
        {
            return this.Pmax;
        }
    }
}
