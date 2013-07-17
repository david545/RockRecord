namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumMemberNoRequierd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.Reviews", "Member_Id", "dbo.Members");
            DropIndex("dbo.Reviews", new[] { "Album_Id" });
            DropIndex("dbo.Reviews", new[] { "Member_Id" });
            AlterColumn("dbo.Reviews", "Album_Id", c => c.Int());
            AlterColumn("dbo.Reviews", "Member_Id", c => c.Int());
            AddForeignKey("dbo.Reviews", "Album_Id", "dbo.Albums", "Id",cascadeDelete:true);
            AddForeignKey("dbo.Reviews", "Member_Id", "dbo.Members", "Id",cascadeDelete:true);
            CreateIndex("dbo.Reviews", "Album_Id");
            CreateIndex("dbo.Reviews", "Member_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "Member_Id" });
            DropIndex("dbo.Reviews", new[] { "Album_Id" });
            DropForeignKey("dbo.Reviews", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.Reviews", "Album_Id", "dbo.Albums");
            AlterColumn("dbo.Reviews", "Member_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Reviews", "Album_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "Member_Id");
            CreateIndex("dbo.Reviews", "Album_Id");
            AddForeignKey("dbo.Reviews", "Member_Id", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Reviews", "Album_Id", "dbo.Albums", "Id", cascadeDelete: true);
        }
    }
}
