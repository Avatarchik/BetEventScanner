namespace BetEventScanner.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: false)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsoCode = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FootballTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                        ShortName = c.String(),
                        StadiumId = c.Int(nullable: false),
                        Stadium_Name = c.String(),
                        Stadium_Capacity = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: false)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: false)
                .Index(t => t.CountryId)
                .Index(t => t.CityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FootballTeams", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.FootballTeams", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.FootballTeams", new[] { "CityId" });
            DropIndex("dbo.FootballTeams", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropTable("dbo.FootballTeams");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
