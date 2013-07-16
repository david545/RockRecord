namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class songHasNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "SongNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "SongNumber");
        }
    }
}
