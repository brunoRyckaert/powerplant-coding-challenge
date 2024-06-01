using PowerPlant.Domain.Models;

namespace PowerPlant.Domain.Logic
{
    public class PowerPlantProductionPlanner
    {
        public IEnumerable<ProductionPlanItem> CalculateProductionPlan(Payload payload)
        {
            var productionPlan = CreatePlan(payload);

            // check if enough power can be produced to satisfy demanded load
            var plannedLoad = productionPlan.Sum(p => p.Power);

            if (plannedLoad < payload.Load)
                throw new ArgumentException($"Insufficient power production to satisfy demanded load: {productionPlan.Sum(p => p.Power)} < {payload.Load}");

            if (plannedLoad != payload.Load)
                throw new ArgumentException($"Impossible to create valid production plan to satisfy demanded load, possibly due to Pmin restrictions: {plannedLoad} > {payload.Load}");

            return productionPlan;
        }
        private static IEnumerable<ProductionPlanItem> CreatePlan(Payload payload)
        {
            var productionPlan = new List<ProductionPlanItem>();
            foreach (var powerPlant in payload.PowerPlants.OrderBy(p => p.CostPerMwh(payload.Fuels)))
            {
                var currentLoad = productionPlan.Sum(p => p.Power);

                var power = 0M;
                if (currentLoad < payload.Load)
                {
                    // take as much as we can without going over the demanded load
                    power = Math.Min(payload.Load - currentLoad, powerPlant.MaxOutput(payload.Fuels));
                    // if we draw power from power plant, we can't draw less than Pmin
                    power = Math.Max(power, powerPlant.Pmin);
                }

                var productionPlanItem =
                    new ProductionPlanItem
                    {
                        Plant = powerPlant,
                        Power = power // don't take more than we need to satisfy the demand
                    };
                productionPlan.Add(productionPlanItem);
            }

            // trim off potential excessive load due to Pmin
            EnsureMinimumProductionRespected(payload, productionPlan);

            return productionPlan;
        }

        private static void EnsureMinimumProductionRespected(Payload payload, IEnumerable<ProductionPlanItem> productionPlan)
        {
            foreach (var planItem in productionPlan.AsEnumerable().Reverse())
            {
                if (productionPlan.Sum(p => p.Power) == payload.Load)
                    continue; // all good, no (more) trimming needed

                // remove excess, but never more than the current plant is already producing
                // if more is needed, it will be removed from the next plant in the next execution of the loop
                planItem.Power -= Math.Min(planItem.Power, productionPlan.Sum(p => p.Power) - payload.Load);
                planItem.Power = Math.Max(planItem.Power, planItem.Plant.Pmin);
            }
        }
    }
}
