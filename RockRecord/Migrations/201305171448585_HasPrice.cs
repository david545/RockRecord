namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HasPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Price");
        }
    }
}
