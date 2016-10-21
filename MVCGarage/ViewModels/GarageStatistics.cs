using System.ComponentModel.DataAnnotations;

namespace MVCGarage.ViewModels
{
    public class GarageStatistics
    {
        public int Id { get; set; }
        [Display(Name = "Count of all vehicles in garage now")]
        public int CountOfVehiclesInGarageNow { get; set; }

        [Display(Name = "Count of all wheels in garage now")]
        public int CountOfWheelsInGarageNow { get; set; }

        [Display(Name = "Parking cost of all vehicles in garage now")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public int ParkingCostOfVehiclesInGarageNow { get; set; }
    }
}