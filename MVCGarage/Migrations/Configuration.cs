namespace MVCGarage.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.MVCGarageDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.MVCGarageDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.VehicleTypes.AddOrUpdate(
              p => p.Type,
              new VehicleType { Type = "Passenger car" },
              new VehicleType { Type = "Truck" },
              new VehicleType { Type = "Buss" }
            );

            context.Vehicles.AddOrUpdate(
              p => p.RegistrationNumber,
              new Vehicle { RegistrationNumber = "UYB123" },
              new Vehicle { RegistrationNumber = "XYB123" },
              new Vehicle { RegistrationNumber = "AYB123" }
            );
            //
        }
        //public class MVCGarageDbInitializer : DropCreateDatabaseIfModelChanges<MVCGarageDbContext>
        //{
        //    protected override void Seed(MVCGarageDbContext context)
        //    {
        //        context.VehicleTypes.AddOrUpdate(v => v.Type,
        //            new VehicleType { Type = "Passenger car" },
        //            new VehicleType { Type = "Truck" },
        //            new VehicleType { Type = "Passenger car" });

        //        context.Vehicles.AddOrUpdate(v => v.RegistrationNumber,
        //            new Vehicle { RegistrationNumber = "UYB123" },
        //            new Vehicle { RegistrationNumber = "YYB123" },
        //            new Vehicle { RegistrationNumber = "AYB123" });

        //        context.SaveChanges();
        //    }
        //}

    }
}
