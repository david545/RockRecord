namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class songAlbumRequest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Songs", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Songs", new[] { "Album_Id" });
            AlterColumn("dbo.Songs", "Album_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.Songs", "Album_Id", "dbo.Albums", "Id", cascadeDelete: true);
            CreateIndex("dbo.Songs", "Album_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Songs", new[] { "Album_Id" });
            DropForeignKey("dbo.Songs", "Album_Id", "dbo.Albums");
            AlterColumn("dbo.Songs", "Album_Id", c => c.Int());
            CreateIndex("dbo.Songs", "Album_Id");
            AddForeignKey("dbo.Songs", "Album_Id", "dbo.Albums", "Id");
        }
    }
}
