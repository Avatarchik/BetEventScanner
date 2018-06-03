namespace BetEventScanner.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BetInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BetInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstPlayer = c.String(maxLength: 50),
                        SecondPlayer = c.String(maxLength: 50),
                        FavoritePlayer = c.Int(nullable: false),
                        WinnerLine = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LineNumber = c.Int(nullable: false),
                        Coefficient = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Score = c.Decimal(precision: 18, scale: 2),
                        BetInfo_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BetInfoes", t => t.BetInfo_Id)
                .Index(t => t.BetInfo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lines", "BetInfo_Id", "dbo.BetInfoes");
            DropIndex("dbo.Lines", new[] { "BetInfo_Id" });
            DropTable("dbo.Lines");
            DropTable("dbo.BetInfoes");
        }
    }
}
