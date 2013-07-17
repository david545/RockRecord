namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReplyComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Replies", "Comment", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Replies", "Comment");
        }
    }
}
