using PowerPlant.Domain.Logic;
using PowerPlant.Domain.Models;
using System.Numerics;

namespace PowerPlant.Tests
{
    public class ProductionPlannerTests
    {
        public readonly PowerPlantProductionPlanner planner = new PowerPlantProductionPlanner();
        public readonly PowerPlantFactory plantFactory = new PowerPlantFactory();
        [Fact]
        public void CheapestFuelUsed()
        {
            var fuels = new Fuels { GasPrice = 1, KerosinePrice = 2 };
            var gasPlant = plantFactory.NewPowerPlant("gasfired");
            var kerosinePlant = plantFactory.NewPowerPlant("turbojet");

            gasPlant.Pmax = kerosinePlant.Pmax = 100;

            var powerplants = new List<PowerPlant.Domain.Models.PowerPlant> { gasPlant, kerosinePlant };

            var payload = new Payload { Fuels = fuels, PowerPlants = powerplants, Load = 100 };

            var plan = planner.CalculateProductionPlan(payload);

            Assert.Equal(100, plan.First(pi => pi.Plant is GasFired).Power);
            Assert.Equal(0, plan.First(pi => pi.Plant is TurboJet).Power);
        }
        [Fact]
        public void MostEfficientPlantUsed()
        {
            var fuels = new Fuels { GasPrice = 1, KerosinePrice = 1 };
            var gasPlant = plantFactory.NewPowerPlant("gasfired");
            var kerosinePlant = plantFactory.NewPowerPlant("turbojet");

            gasPlant.Name = "gas";
            gasPlant.Efficiency = 0.75M;
            kerosinePlant.Name = "kerosine";
            kerosinePlant.Efficiency = 0.50M;

            gasPlant.Pmax = kerosinePlant.Pmax = 100;

            var powerplants = new List<PowerPlant.Domain.Models.PowerPlant> { gasPlant, kerosinePlant };

            var payload = new Payload { Fuels = fuels, PowerPlants = powerplants, Load = 100 };

            var plan = planner.CalculateProductionPlan(payload);

            Assert.Equal(100, plan.First(pi => pi.Plant is GasFired).Power);
            Assert.Equal(0, plan.First(pi => pi.Plant is TurboJet).Power);
        }
        [Fact]
        public void MaximizeMostEfficientPlantBeforeOther()
        {
            var fuels = new Fuels { GasPrice = 1, KerosinePrice = 1 };
            var gasPlant = plantFactory.NewPowerPlant("gasfired");
            var kerosinePlant = plantFactory.NewPowerPlant("turbojet");

            gasPlant.Name = "gas";
            gasPlant.Efficiency = 0.75M;
            kerosinePlant.Name = "kerosine";
            kerosinePlant.Efficiency = 0.50M;

            gasPlant.Pmax = kerosinePlant.Pmax = 100;

            var powerplants = new List<PowerPlant.Domain.Models.PowerPlant> { gasPlant, kerosinePlant };

            var payload = new Payload { Fuels = fuels, PowerPlants = powerplants, Load = 150 };

            var plan = planner.CalculateProductionPlan(payload);

            Assert.Equal(100, plan.First(pi => pi.Plant is GasFired).Power);
            Assert.Equal(50, plan.First(pi => pi.Plant is TurboJet).Power);
        }
        [Fact]
        public void SufficientPowerAvailable()
        {
            // enough power is available, even though efficiency isn't 1

            var fuels = new Fuels { GasPrice = 1, KerosinePrice = 1 };
            var gasPlant = plantFactory.NewPowerPlant("gasfired");
            var kerosinePlant = plantFactory.NewPowerPlant("turbojet");

            gasPlant.Name = "gas";
            gasPlant.Efficiency = 0.75M;
            kerosinePlant.Name = "kerosine";
            kerosinePlant.Efficiency = 0.50M;

            gasPlant.Pmax = kerosinePlant.Pmax = 100;

            var powerplants = new List<PowerPlant.Domain.Models.PowerPlant> { gasPlant, kerosinePlant };

            var payload = new Payload { Fuels = fuels, PowerPlants = powerplants, Load = 200 };

            var plan = planner.CalculateProductionPlan(payload);

            Assert.Equal(100, plan.First(pi => pi.Plant is GasFired).Power);
            Assert.Equal(100, plan.First(pi => pi.Plant is TurboJet).Power);
            Assert.Equal(200, plan.Sum(pi => pi.Power));
        }
        [Fact]
        public void ThrowOnInsufficientAvailablePower()
        {
            var fuels = new Fuels { GasPrice = 1, KerosinePrice = 1 };
            var gasPlant = plantFactory.NewPowerPlant("gasfired");
            var kerosinePlant = plantFactory.NewPowerPlant("turbojet");

            gasPlant.Name = "gas";
            gasPlant.Efficiency = 0.75M;
            kerosinePlant.Name = "kerosine";
            kerosinePlant.Efficiency = 0.50M;

            gasPlant.Pmax = kerosinePlant.Pmax = 100;

            var powerplants = new List<PowerPlant.Domain.Models.PowerPlant> { gasPlant, kerosinePlant };

            var payload = new Payload { Fuels = fuels, PowerPlants = powerplants, Load = 201 };

            Assert.Throws<ArgumentException>(() => planner.CalculateProductionPlan(payload));
        }
        [Fact]
        public void HandlePminCorrectly()
        {
            var fuels = new Fuels { GasPrice = 1, KerosinePrice = 1 };
            var gasPlant = plantFactory.NewPowerPlant("gasfired");
            var kerosinePlant = plantFactory.NewPowerPlant("turbojet");

            gasPlant.Name = "gas";
            gasPlant.Efficiency = 0.75M;
            kerosinePlant.Name = "kerosine";
            kerosinePlant.Efficiency = 0.50M;
            kerosinePlant.Pmin = 10;

            gasPlant.Pmax = kerosinePlant.Pmax = 100;

            var powerplants = new List<PowerPlant.Domain.Models.PowerPlant> { gasPlant, kerosinePlant };

            var payload = new Payload { Fuels = fuels, PowerPlants = powerplants, Load = 105 };

            var plan = planner.CalculateProductionPlan(payload);

            Assert.Equal(95, plan.First(pi => pi.Plant is GasFired).Power);
            Assert.Equal(10, plan.First(pi => pi.Plant is TurboJet).Power);
            Assert.Equal(105, plan.Sum(pi => pi.Power));
        }
        [Fact]
        public void Co2Cost()
        {
            var fuels = new Fuels { GasPrice = 1, Co2Price = 3};
            var gasPlant = plantFactory.NewPowerPlant("gasfired");

            gasPlant.Name = "gas";
            gasPlant.Efficiency = 0.5M;

            var costPerMwh = gasPlant.CostPerMwh(fuels);

            // efficiency of .5M, so 2 times the gas price = 2;
            // co2price of 3 per tonne, and 0.3 tonnes per mwh = 0.9;
            // total = 2.9
            Assert.Equal(2.9M, costPerMwh);
        }

    }
}