namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class REmoveCategoryRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "AlbumCategory_Id", "dbo.AlbumCategories");
            DropIndex("dbo.Albums", new[] { "AlbumCategory_Id" });
            AddForeignKey("dbo.Albums", "AlbumCategory_Id", "dbo.AlbumCategories", "Id");
            CreateIndex("dbo.Albums", "AlbumCategory_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Albums", new[] { "AlbumCategory_Id" });
            DropForeignKey("dbo.Albums", "AlbumCategory_Id", "dbo.AlbumCategories");
            CreateIndex("dbo.Albums", "AlbumCategory_Id");
            AddForeignKey("dbo.Albums", "AlbumCategory_Id", "dbo.AlbumCategories", "Id", cascadeDelete: true);
        }
    }
}
