namespace MVCGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GarageStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountOfVehiclesInGarageNow = c.Int(nullable: false),
                        CountOfWheelsInGarageNow = c.Int(nullable: false),
                        ParkingCostOfVehiclesInGarageNow = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GarageStatistics");
        }
    }
}
