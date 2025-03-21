namespace Dog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStatus", c => c.Int());
            AlterColumn("dbo.Photos", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.Photos", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Employees", "CreateDate", c => c.DateTime());
            DropColumn("dbo.Orders", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Status", c => c.Int());
            AlterColumn("dbo.Employees", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Photos", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Photos", "CreatedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "OrderStatus");
        }
    }
}
