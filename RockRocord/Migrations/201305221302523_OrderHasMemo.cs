namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderHasMemo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderHeaders", "Memo", c => c.String(maxLength: 250));
            AddColumn("dbo.OrderHeaders", "OrderStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderHeaders", "OrderStatus");
            DropColumn("dbo.OrderHeaders", "Memo");
        }
    }
}
