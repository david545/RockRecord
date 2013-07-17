namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeOrderSystem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderHeaders", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.OrderDetails", "OrderHeader_Id", "dbo.OrderHeaders");
            DropForeignKey("dbo.OrderDetails", "Album_Id", "dbo.Albums");
            DropIndex("dbo.OrderHeaders", new[] { "Member_Id" });
            DropIndex("dbo.OrderDetails", new[] { "OrderHeader_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Album_Id" });
            DropTable("dbo.OrderHeaders");
            DropTable("dbo.OrderDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        OrderHeader_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactName = c.String(nullable: false, maxLength: 250),
                        ContactPhoneNo = c.String(nullable: false, maxLength: 25),
                        ContactAddress = c.String(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        Memo = c.String(),
                        BuyDate = c.DateTime(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.OrderDetails", "Album_Id");
            CreateIndex("dbo.OrderDetails", "OrderHeader_Id");
            CreateIndex("dbo.OrderHeaders", "Member_Id");
            AddForeignKey("dbo.OrderDetails", "Album_Id", "dbo.Albums", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "OrderHeader_Id", "dbo.OrderHeaders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderHeaders", "Member_Id", "dbo.Members", "Id", cascadeDelete: true);
        }
    }
}
