using System.Text.Json.Serialization;

namespace PowerPlant.Domain.Models
{
    public class ProductionPlanItem
    {
        public PowerPlant Plant { get; set; }
        public decimal Power { get; set; }
    }
}
