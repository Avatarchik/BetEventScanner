namespace BetEventScanner.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlatBets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tournament = c.String(),
                        Player1Name = c.String(),
                        Player2Name = c.String(),
                        SK1 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SK1Descr = c.String(),
                        SK2 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SK2Descr = c.String(),
                        SK3 = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SK3Descr = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IncomingMatches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Div = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        HomeTeam = c.String(),
                        AwayTeam = c.String(),
                        HomeScored = c.Int(),
                        AwayScored = c.Int(),
                        HomeOdds = c.Double(nullable: false),
                        DrawOdds = c.Double(nullable: false),
                        AwayOdds = c.Double(nullable: false),
                        Over25Odds = c.Double(nullable: false),
                        Under25Odds = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FootballMatchOdds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeWin = c.Double(nullable: false),
                        Draw = c.Double(nullable: false),
                        AwayWin = c.Double(nullable: false),
                        HomeWinOrDraw = c.Double(nullable: false),
                        NoDraw = c.Double(nullable: false),
                        AwayWinOrDraw = c.Double(nullable: false),
                        Over25 = c.Double(nullable: false),
                        Under25 = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FootballMatchResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeasonId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        HomeTeam = c.String(),
                        AwayTeam = c.String(),
                        HomeScored = c.Int(nullable: false),
                        AwayScored = c.Int(nullable: false),
                        OddsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FootballMatchOdds", t => t.OddsId, cascadeDelete: true)
                .ForeignKey("dbo.FootballSeasons", t => t.SeasonId, cascadeDelete: true)
                .Index(t => t.SeasonId)
                .Index(t => t.OddsId);
            
            CreateTable(
                "dbo.FootballSeasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        CountryCode = c.String(),
                        Division = c.String(),
                        DivisionCode = c.String(),
                        StartYear = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FootballMatchResults", "SeasonId", "dbo.FootballSeasons");
            DropForeignKey("dbo.FootballMatchResults", "OddsId", "dbo.FootballMatchOdds");
            DropIndex("dbo.FootballMatchResults", new[] { "OddsId" });
            DropIndex("dbo.FootballMatchResults", new[] { "SeasonId" });
            DropTable("dbo.FootballSeasons");
            DropTable("dbo.FootballMatchResults");
            DropTable("dbo.FootballMatchOdds");
            DropTable("dbo.IncomingMatches");
            DropTable("dbo.FlatBets");
        }
    }
}
