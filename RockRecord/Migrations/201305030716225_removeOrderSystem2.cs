namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeOrderSystem2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Price", c => c.Int(nullable: false));
        }
    }
}
