namespace BetEventScanner.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSetsToFlatBetResult : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FlatBetResults", "Set1", c => c.String());
            AddColumn("dbo.FlatBetResults", "Set2", c => c.String());
            AddColumn("dbo.FlatBetResults", "Set3", c => c.String());
            AddColumn("dbo.FlatBetResults", "Set4", c => c.String());
            AddColumn("dbo.FlatBetResults", "Set5", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FlatBetResults", "Set5");
            DropColumn("dbo.FlatBetResults", "Set4");
            DropColumn("dbo.FlatBetResults", "Set3");
            DropColumn("dbo.FlatBetResults", "Set2");
            DropColumn("dbo.FlatBetResults", "Set1");
        }
    }
}
