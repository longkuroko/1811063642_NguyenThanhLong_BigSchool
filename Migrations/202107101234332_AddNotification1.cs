namespace BigSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotification1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers");
            AddColumn("dbo.Notifications", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notifications", "CourseId");
            AddForeignKey("dbo.Notifications", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notifications", "CourseId", "dbo.Courses");
            DropIndex("dbo.Notifications", new[] { "CourseId" });
            DropColumn("dbo.Notifications", "CourseId");
            AddForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
