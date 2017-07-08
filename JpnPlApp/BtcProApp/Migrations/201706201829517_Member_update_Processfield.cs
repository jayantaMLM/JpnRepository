namespace BtcProApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Member_update_Processfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "ProcessNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "ProcessNo");
        }
    }
}
