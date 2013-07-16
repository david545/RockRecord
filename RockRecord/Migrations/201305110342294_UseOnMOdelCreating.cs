namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UseOnMOdelCreating : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Album_Id", "dbo.Albums");
            DropIndex("dbo.Reviews", new[] { "Album_Id" });
            AlterColumn("dbo.Reviews", "Album_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.Reviews", "Album_Id", "dbo.Albums", "Id", cascadeDelete: true);
            CreateIndex("dbo.Reviews", "Album_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "Album_Id" });
            DropForeignKey("dbo.Reviews", "Album_Id", "dbo.Albums");
            AlterColumn("dbo.Reviews", "Album_Id", c => c.Int());
            CreateIndex("dbo.Reviews", "Album_Id");
            AddForeignKey("dbo.Reviews", "Album_Id", "dbo.Albums", "Id");
        }
    }
}
