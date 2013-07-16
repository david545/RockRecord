namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hasforeignKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Albums", name: "Artist_Id", newName: "ArtistId");
            RenameColumn(table: "dbo.Albums", name: "AlbumCategory_Id", newName: "AlbumCategoryId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Albums", name: "AlbumCategoryId", newName: "AlbumCategory_Id");
            RenameColumn(table: "dbo.Albums", name: "ArtistId", newName: "Artist_Id");
        }
    }
}
