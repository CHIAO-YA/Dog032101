namespace Dog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nill : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Longitude", c => c.Decimal(precision: 18, scale: 6));
            AlterColumn("dbo.Orders", "Latitude", c => c.Decimal(precision: 18, scale: 6));
            AlterColumn("dbo.Orders", "WeekDay", c => c.Int());
            AlterColumn("dbo.Orders", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "EndDate", c => c.DateTime());
            AlterColumn("dbo.Orders", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.Orders", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Orders", "Status", c => c.Int());
            AlterColumn("dbo.Orders", "PaymentStatus", c => c.Int());
            AlterColumn("dbo.Orders", "KG", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "ReportedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ReportedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "KG", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Orders", "PaymentStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Orders", "WeekDay", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 6));
            AlterColumn("dbo.Orders", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 6));
        }
    }
}
