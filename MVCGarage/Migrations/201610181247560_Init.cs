namespace MVCGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleTypes", "Type", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.VehicleTypes", "Type", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.VehicleTypes", new[] { "Type" });
            AlterColumn("dbo.VehicleTypes", "Type", c => c.String(nullable: false));
        }
    }
}
