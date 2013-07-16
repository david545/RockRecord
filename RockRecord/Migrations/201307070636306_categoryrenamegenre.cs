namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryrenamegenre : DbMigration
    {
        public override void Up()
        {
            
            RenameColumn(table: "dbo.Albums", name: "AlbumCategoryId", newName: "GenreId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Albums", name: "GenreId", newName: "AlbumCategoryId");
        }
    }
}
