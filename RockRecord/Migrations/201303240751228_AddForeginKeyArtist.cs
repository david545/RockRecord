namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeginKeyArtist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.Albums", new[] { "Artist_Id" });
            AlterColumn("dbo.Albums", "Artist_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists", "Id", cascadeDelete: true);
            CreateIndex("dbo.Albums", "Artist_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Albums", new[] { "Artist_Id" });
            DropForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists");
            AlterColumn("dbo.Albums", "Artist_Id", c => c.Int());
            CreateIndex("dbo.Albums", "Artist_Id");
            AddForeignKey("dbo.Albums", "Artist_Id", "dbo.Artists", "Id");
        }
    }
}
