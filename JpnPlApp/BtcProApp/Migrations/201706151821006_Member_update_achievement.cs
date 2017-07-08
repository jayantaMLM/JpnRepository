namespace BtcProApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Member_update_achievement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Achievement1", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "Achievement2", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Achievement2");
            DropColumn("dbo.Members", "Achievement1");
        }
    }
}
