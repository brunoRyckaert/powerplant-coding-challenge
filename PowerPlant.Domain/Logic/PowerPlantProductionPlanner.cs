using PowerPlant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPlant.Domain.Logic
{
    public class PowerPlantProductionPlanner
    {
        public IEnumerable<ProductionPlanItem> CalculateProductionPlan(Payload payload)
        {
            var productionPlan = new List<ProductionPlanItem>();
            foreach (var powerPlant in payload.PowerPlants)
            {
                var currentLoad = productionPlan.Sum(p => p.Power);
                if (currentLoad > payload.Load)
                    throw new InvalidOperationException($"Current load exceeds demanded load : {currentLoad} > {payload.Load} ");
                if (currentLoad == payload.Load)
                    continue;
                
                productionPlan.Add(new ProductionPlanItem { Name = powerPlant.Name, Power = Math.Min(payload.Load - currentLoad, powerPlant.Pmax ) });
                
            }

            if (productionPlan.Sum(p => p.Power) < payload.Load)
                throw new ArgumentException($"Insufficient power production to satisfy demanded load: {productionPlan.Sum(p => p.Power)} < {payload.Load}");

            return productionPlan;
        }
    }
}
