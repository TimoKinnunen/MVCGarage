using System.Data.Entity;

namespace MVCGarage.DAL
{
    public class MVCGarageDbContext : DbContext
    {
        public MVCGarageDbContext() : base("MVCGarageDatabase")
        {
            
        }
        public DbSet<Models.Vehicle> Vehicles { get; set; }
        public DbSet<Models.VehicleType> VehicleTypes { get; set; }
    }
}