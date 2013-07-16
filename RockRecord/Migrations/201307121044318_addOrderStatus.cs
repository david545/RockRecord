namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOrderStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.OrderHeaders", "OrderStatus_Id", c => c.Int());
            AddForeignKey("dbo.OrderHeaders", "OrderStatus_Id", "dbo.OrderStatus", "Id");
            CreateIndex("dbo.OrderHeaders", "OrderStatus_Id");
            DropColumn("dbo.OrderHeaders", "OrderStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderHeaders", "OrderStatus", c => c.Int(nullable: false));
            DropIndex("dbo.OrderHeaders", new[] { "OrderStatus_Id" });
            DropForeignKey("dbo.OrderHeaders", "OrderStatus_Id", "dbo.OrderStatus");
            DropColumn("dbo.OrderHeaders", "OrderStatus_Id");
            DropTable("dbo.OrderStatus");
        }
    }
}
