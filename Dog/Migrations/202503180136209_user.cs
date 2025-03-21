namespace Dog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UsersID = c.Int(nullable: false, identity: true),
                        Number = c.String(maxLength: 20),
                        Roles = c.Int(nullable: false),
                        Name = c.String(maxLength: 20),
                        LinePicUrl = c.String(maxLength: 255),
                        Phone = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UsersID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
