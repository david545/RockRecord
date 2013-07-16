namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alsobought : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Album_Id", c => c.Int());
            AddForeignKey("dbo.Albums", "Album_Id", "dbo.Albums", "Id");
            CreateIndex("dbo.Albums", "Album_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Albums", new[] { "Album_Id" });
            DropForeignKey("dbo.Albums", "Album_Id", "dbo.Albums");
            DropColumn("dbo.Albums", "Album_Id");
        }
    }
}
