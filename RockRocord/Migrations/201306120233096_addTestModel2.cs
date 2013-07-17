namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTestModel2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guys", "Name", c => c.String());
            AddColumn("dbo.Girls", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Girls", "Name");
            DropColumn("dbo.Guys", "Name");
        }
    }
}
