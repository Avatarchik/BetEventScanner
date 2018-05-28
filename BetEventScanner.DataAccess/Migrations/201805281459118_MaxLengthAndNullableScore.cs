namespace BetEventScanner.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MaxLengthAndNullableScore : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BetInfoes", "FirstPlayer", c => c.String(maxLength: 50));
            AlterColumn("dbo.BetInfoes", "SecondPlayer", c => c.String(maxLength: 50));
            AlterColumn("dbo.Lines", "Score", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lines", "Score", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.BetInfoes", "SecondPlayer", c => c.String());
            AlterColumn("dbo.BetInfoes", "FirstPlayer", c => c.String());
        }
    }
}
