namespace Dog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class all : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrdersID = c.Int(nullable: false, identity: true),
                        UsersID = c.Int(nullable: false),
                        PlanID = c.Int(nullable: false),
                        DiscountID = c.Int(nullable: false),
                        OrderName = c.String(maxLength: 50),
                        OrderPhone = c.String(maxLength: 20),
                        Addresses = c.String(maxLength: 255),
                        Notes = c.String(),
                        WeekDay = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        PaymentStatus = c.Int(nullable: false),
                        KG = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IssueDescription = c.String(maxLength: 500),
                        ReportedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrdersID)
                .ForeignKey("dbo.Discounts", t => t.DiscountID, cascadeDelete: true)
                .ForeignKey("dbo.Plans", t => t.PlanID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UsersID, cascadeDelete: true)
                .Index(t => t.UsersID)
                .Index(t => t.PlanID)
                .Index(t => t.DiscountID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoID = c.Int(nullable: false, identity: true),
                        OrdersID = c.Int(nullable: false),
                        OrderImageUrl = c.String(maxLength: 255),
                        DrivermageUrl = c.String(maxLength: 255),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoID)
                .ForeignKey("dbo.Orders", t => t.OrdersID, cascadeDelete: true)
                .Index(t => t.OrdersID);
            
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        PlanID = c.Int(nullable: false, identity: true),
                        PlanName = c.String(maxLength: 20),
                        Liter = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlanID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UsersID", "dbo.Users");
            DropForeignKey("dbo.Orders", "PlanID", "dbo.Plans");
            DropForeignKey("dbo.Photos", "OrdersID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "DiscountID", "dbo.Discounts");
            DropIndex("dbo.Photos", new[] { "OrdersID" });
            DropIndex("dbo.Orders", new[] { "DiscountID" });
            DropIndex("dbo.Orders", new[] { "PlanID" });
            DropIndex("dbo.Orders", new[] { "UsersID" });
            DropTable("dbo.Plans");
            DropTable("dbo.Photos");
            DropTable("dbo.Orders");
        }
    }
}
