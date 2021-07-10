namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Notifications", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Type", c => c.Int(nullable: false));
        }
    }
}
