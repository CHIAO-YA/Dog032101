namespace Dog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DiscountEmployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        DiscountID = c.Int(nullable: false, identity: true),
                        Months = c.Int(nullable: false),
                        DiscountRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.DiscountID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        Identity = c.Int(nullable: false),
                        Account = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Salt = c.String(nullable: false, maxLength: 100),
                        Name = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .Index(t => t.Account, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Employees", new[] { "Account" });
            DropTable("dbo.Employees");
            DropTable("dbo.Discounts");
        }
    }
}
