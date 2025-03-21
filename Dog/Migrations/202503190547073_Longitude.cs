namespace Dog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Longitude : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AddColumn("dbo.Orders", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 6));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Latitude");
            DropColumn("dbo.Orders", "Longitude");
        }
    }
}
