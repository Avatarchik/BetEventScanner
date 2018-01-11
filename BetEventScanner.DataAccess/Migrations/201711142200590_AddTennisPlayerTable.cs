namespace BetEventScanner.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTennisPlayerTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TennisPlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OriginPlayerId = c.String(),
                        Sex = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Hand = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        СountryCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TennisPlayers");
        }
    }
}
