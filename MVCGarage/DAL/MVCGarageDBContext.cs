using MVCGarage.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;

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