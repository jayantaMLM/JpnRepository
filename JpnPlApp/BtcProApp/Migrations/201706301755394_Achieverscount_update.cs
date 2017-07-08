namespace BtcProApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Achieverscount_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Achievers1CountTeamLeftside", c => c.Int(nullable: false));
            AddColumn("dbo.Members", "Achievers1CountTeamRightside", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Achievers1CountTeamRightside");
            DropColumn("dbo.Members", "Achievers1CountTeamLeftside");
        }
    }
}
