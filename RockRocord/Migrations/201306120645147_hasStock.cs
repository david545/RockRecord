namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hasStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Stock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Stock");
        }
    }
}
