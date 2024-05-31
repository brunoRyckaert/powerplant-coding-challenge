using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlant.Domain.Models
{
    public class PowerPlantFactory
    {
        public PowerPlant NewPowerPlant(string type)
        {
            switch (type)
            {
                case "gasfired":
                    return new GasFired();
                case "turbojet":
                    return new TurboJet();
                case "windturbine":
                    return new WindTurbine();
                default:
                    throw new ArgumentException($"Unknown powerplant type provided: {type}");
            }
        }
    }
}
