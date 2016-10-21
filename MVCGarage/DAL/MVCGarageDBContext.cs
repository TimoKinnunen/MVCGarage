using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MVCGarage.DAL
{
    public class MVCGarageDbContext : DbContext
    {
        public MVCGarageDbContext() : base("MVCGarageDatabase")
        {
            
        }
        public DbSet<Models.Vehicle> Vehicles { get; set; }
        public DbSet<Models.VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}