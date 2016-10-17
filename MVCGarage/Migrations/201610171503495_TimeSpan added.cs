namespace MVCGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeSpanadded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "ParkingTime", c => c.Time(precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "ParkingTime", c => c.DateTimeOffset(precision: 7));
        }
    }
}
