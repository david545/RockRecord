namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removealsobought : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Albums", new[] { "Album_Id" });
            DropColumn("dbo.Albums", "Album_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Album_Id", c => c.Int());
            CreateIndex("dbo.Albums", "Album_Id");
            AddForeignKey("dbo.Albums", "Album_Id", "dbo.Albums", "Id");
        }
    }
}
