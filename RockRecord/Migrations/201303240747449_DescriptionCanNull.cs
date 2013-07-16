namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionCanNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Albums", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Albums", "Description", c => c.String(nullable: false));
        }
    }
}
