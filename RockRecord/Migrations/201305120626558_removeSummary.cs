namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeSummary : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reviews", "Summary");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "Summary", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
