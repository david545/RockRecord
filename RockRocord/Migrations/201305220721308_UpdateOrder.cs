namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderHeaders", "Zipcode", c => c.Int(nullable: false));
            AddColumn("dbo.OrderHeaders", "City", c => c.String(nullable: false));
            AddColumn("dbo.OrderHeaders", "State", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderHeaders", "State");
            DropColumn("dbo.OrderHeaders", "City");
            DropColumn("dbo.OrderHeaders", "Zipcode");
        }
    }
}
