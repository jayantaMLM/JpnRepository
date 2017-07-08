namespace BtcProApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Member_addfields_AchievementDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Achievement1Date", c => c.DateTime());
            AddColumn("dbo.Members", "Achievement2Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Achievement2Date");
            DropColumn("dbo.Members", "Achievement1Date");
        }
    }
}
