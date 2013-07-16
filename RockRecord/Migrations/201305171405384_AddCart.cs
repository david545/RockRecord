namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderHeaders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        ContactName = c.String(nullable: false, maxLength: 40),
                        ContactAddress = c.String(nullable: false),
                        ContactPhone = c.String(nullable: false, maxLength: 25),
                        TotalPrice = c.Int(nullable: false),
                        Member_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Album_Id = c.Int(),
                        OrderHeader_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.Album_Id)
                .ForeignKey("dbo.OrderHeaders", t => t.OrderHeader_Id, cascadeDelete: true)
                .Index(t => t.Album_Id)
                .Index(t => t.OrderHeader_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.OrderDetails", new[] { "OrderHeader_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Album_Id" });
            DropIndex("dbo.OrderHeaders", new[] { "Member_Id" });
            DropForeignKey("dbo.OrderDetails", "OrderHeader_Id", "dbo.OrderHeaders");
            DropForeignKey("dbo.OrderDetails", "Album_Id", "dbo.Albums");
            DropForeignKey("dbo.OrderHeaders", "Member_Id", "dbo.Members");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.OrderHeaders");
        }
    }
}
