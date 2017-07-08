namespace BtcProApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BinaryOpenings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RegistrationId = c.Long(nullable: false),
                        LeftSideCd = c.Double(nullable: false),
                        RightSideCd = c.Double(nullable: false),
                        ProcessId = c.String(),
                        IsCurrent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WithdrawalRequests",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RegistrationId = c.Long(nullable: false),
                        WalletId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        sDate = c.String(),
                        Amount = c.Double(nullable: false),
                        Approved_Date = c.DateTime(),
                        sApproved_Date = c.String(),
                        Status = c.String(),
                        PaidOutAmount = c.Double(nullable: false),
                        ServiceCharge = c.Double(nullable: false),
                        BatchNo = c.String(),
                        ReferenceNo = c.String(),
                        PaidBitCoinAccount = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.BinaryIncomes", "ProcessId", c => c.String());
            AddColumn("dbo.GenerationIncomes", "ProcessId", c => c.String());
            AddColumn("dbo.Ledgers", "ProcessId", c => c.String());
            AddColumn("dbo.Ledgers", "Leftside_cd", c => c.Double());
            AddColumn("dbo.Ledgers", "Rightside_cd", c => c.Double());
            AddColumn("dbo.Registers", "MyWalletAccount", c => c.String());
            AddColumn("dbo.Registers", "WorkingLeg", c => c.String());
            AddColumn("dbo.SponsorIncomes", "ProcessId", c => c.String());
            AddColumn("dbo.WeeklyIncomes", "ProcessId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WeeklyIncomes", "ProcessId");
            DropColumn("dbo.SponsorIncomes", "ProcessId");
            DropColumn("dbo.Registers", "WorkingLeg");
            DropColumn("dbo.Registers", "MyWalletAccount");
            DropColumn("dbo.Ledgers", "Rightside_cd");
            DropColumn("dbo.Ledgers", "Leftside_cd");
            DropColumn("dbo.Ledgers", "ProcessId");
            DropColumn("dbo.GenerationIncomes", "ProcessId");
            DropColumn("dbo.BinaryIncomes", "ProcessId");
            DropTable("dbo.WithdrawalRequests");
            DropTable("dbo.BinaryOpenings");
        }
    }
}
