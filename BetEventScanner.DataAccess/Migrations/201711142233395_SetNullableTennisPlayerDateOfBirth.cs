namespace BetEventScanner.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetNullableTennisPlayerDateOfBirth : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TennisPlayers", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TennisPlayers", "BirthDate", c => c.DateTime(nullable: false));
        }
    }
}
