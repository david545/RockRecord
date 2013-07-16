namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSummary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Summary", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Summary");
        }
    }
}
