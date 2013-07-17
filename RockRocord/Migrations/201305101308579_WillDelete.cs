namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WillDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Member_Id", "dbo.Members");
            DropIndex("dbo.Reviews", new[] { "Member_Id" });
            AlterColumn("dbo.Reviews", "Member_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.Reviews", "Member_Id", "dbo.Members", "Id", cascadeDelete: true);
            CreateIndex("dbo.Reviews", "Member_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "Member_Id" });
            DropForeignKey("dbo.Reviews", "Member_Id", "dbo.Members");
            AlterColumn("dbo.Reviews", "Member_Id", c => c.Int());
            CreateIndex("dbo.Reviews", "Member_Id");
            AddForeignKey("dbo.Reviews", "Member_Id", "dbo.Members", "Id");
        }
    }
}
