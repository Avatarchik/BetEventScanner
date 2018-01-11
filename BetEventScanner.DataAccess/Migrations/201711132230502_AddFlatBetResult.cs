namespace BetEventScanner.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlatBetResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlatBetResults",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FlatBets", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FlatBetResults", "Id", "dbo.FlatBets");
            DropIndex("dbo.FlatBetResults", new[] { "Id" });
            DropTable("dbo.FlatBetResults");
        }
    }
}
