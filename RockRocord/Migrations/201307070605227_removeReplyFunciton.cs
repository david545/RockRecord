namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeReplyFunciton : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Replies", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.Replies", "Review_Id", "dbo.Reviews");
            DropIndex("dbo.Replies", new[] { "Member_Id" });
            DropIndex("dbo.Replies", new[] { "Review_Id" });
            DropTable("dbo.Replies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReplyDateTime = c.DateTime(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 300),
                        Member_Id = c.Int(),
                        Review_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Replies", "Review_Id");
            CreateIndex("dbo.Replies", "Member_Id");
            AddForeignKey("dbo.Replies", "Review_Id", "dbo.Reviews", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Replies", "Member_Id", "dbo.Members", "Id");
        }
    }
}
