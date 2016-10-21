namespace MVCGarage.Migrations
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    internal sealed class Configuration : DbMigrationsConfiguration<DAL.MVCGarageDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.MVCGarageDbContext context)
        {
            context.VehicleTypes.AddOrUpdate(
              p => p.Type,
              new VehicleType { Type = "Passenger car" },
              new VehicleType { Type = "Truck" },
              new VehicleType { Type = "Bus" }
            );


            context.SaveChanges();

            var vehicleTypes = context.VehicleTypes.ToList();

            context.Vehicles.AddOrUpdate(
              p => p.RegistrationNumber,
              new Vehicle
              {
                  RegistrationNumber = "UYB123",
                  StartParkingTime = DateTime.Now.AddHours(-1),
                  ParkingCostPerHour = 60,
                  VehicleTypeId = vehicleTypes[0].Id
              },
              new Vehicle
              {
                  RegistrationNumber = "XYB123",
                  StartParkingTime = DateTime.Now.AddHours(-1),
                  ParkingCostPerHour = 60,
                  VehicleTypeId = vehicleTypes[0].Id
              },
              new Vehicle
              {
                  RegistrationNumber = "AYB123",
                  StartParkingTime = DateTime.Now.AddHours(-1),
                  ParkingCostPerHour = 60,
                  VehicleTypeId = vehicleTypes[0].Id
              }
            );
        }
    }
}
