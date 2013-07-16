namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderDetailAddAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "Amount");
        }
    }
}
