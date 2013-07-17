namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Rating = c.Int(nullable: false),
                        ReviewDate = c.DateTime(nullable: false),
                        Album_Id = c.Int(),
                        Member_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .ForeignKey("dbo.Members", t => t.Member_Id)
                .Index(t => t.Album_Id)
                .Index(t => t.Member_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reviews", new[] { "Member_Id" });
            DropIndex("dbo.Reviews", new[] { "Album_Id" });
            DropForeignKey("dbo.Reviews", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.Reviews", "Album_Id", "dbo.Albums");
            DropTable("dbo.Reviews");
        }
    }
}
