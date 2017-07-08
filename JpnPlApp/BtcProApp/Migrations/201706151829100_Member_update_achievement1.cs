namespace BtcProApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Member_update_achievement1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Achievers1CountTeam", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Achievers1CountTeam");
        }
    }
}
