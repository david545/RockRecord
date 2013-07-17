namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reviewcommentRestrict : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Comment", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Comment", c => c.String(nullable: false));
        }
    }
}
