namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryrenamegenre2 : DbMigration
    {
        public override void Up()
        {
            RenameTable("AlbumCategories", "Genres");
        }
        
        public override void Down()
        {
           
        }
    }
}
