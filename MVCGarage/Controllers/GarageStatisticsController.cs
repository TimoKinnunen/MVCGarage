using MVCGarage.DAL;
using MVCGarage.ViewModels;
using System;
using System.Web.Mvc;

namespace MVCGarage.Controllers
{
    public class GarageStatisticsController : Controller
    {
        private MVCGarageDbContext db = new MVCGarageDbContext();

        // GET: GarageStatistics
        public ActionResult Index()
        {
            int countOfVehiclesInGarageNow = 0;
            int countOfWheelsInGarageNow = 0;
            int parkingCostOfVehiclesInGarageNow = 0;

            var query = db.Vehicles;
            foreach (var vehicle in query)
            {
                countOfVehiclesInGarageNow++;
                if (vehicle.NumberOfWheels.HasValue)
                {
                    countOfWheelsInGarageNow = countOfWheelsInGarageNow + (int)vehicle.NumberOfWheels;
                }

                if (vehicle.ParkingCost.HasValue)
                {
                    parkingCostOfVehiclesInGarageNow = parkingCostOfVehiclesInGarageNow + (int)vehicle.ParkingCost;

                }
            }

            GarageStatistics garageStatistics = new GarageStatistics
            {
                CountOfVehiclesInGarageNow = countOfVehiclesInGarageNow,
                CountOfWheelsInGarageNow = countOfWheelsInGarageNow,
                ParkingCostOfVehiclesInGarageNow = parkingCostOfVehiclesInGarageNow
            };
            return View(garageStatistics);
        }
    }
}
