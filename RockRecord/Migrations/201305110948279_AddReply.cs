namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReply : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReplyDateTime = c.DateTime(nullable: false),
                        Member_Id = c.Int(),
                        Review_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Member_Id)
                .ForeignKey("dbo.Reviews", t => t.Review_Id, cascadeDelete: true)
                .Index(t => t.Member_Id)
                .Index(t => t.Review_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Replies", new[] { "Review_Id" });
            DropIndex("dbo.Replies", new[] { "Member_Id" });
            DropForeignKey("dbo.Replies", "Review_Id", "dbo.Reviews");
            DropForeignKey("dbo.Replies", "Member_Id", "dbo.Members");
            DropTable("dbo.Replies");
        }
    }
}
