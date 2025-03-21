namespace Dog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class weekday : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "WeekDay", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "WeekDay", c => c.Int());
        }
    }
}
