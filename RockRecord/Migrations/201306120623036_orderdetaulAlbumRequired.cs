namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderdetaulAlbumRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Album_Id", "dbo.Albums");
            DropIndex("dbo.OrderDetails", new[] { "Album_Id" });
            AlterColumn("dbo.OrderDetails", "Album_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.OrderDetails", "Album_Id", "dbo.Albums", "Id", cascadeDelete: true);
            CreateIndex("dbo.OrderDetails", "Album_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.OrderDetails", new[] { "Album_Id" });
            DropForeignKey("dbo.OrderDetails", "Album_Id", "dbo.Albums");
            AlterColumn("dbo.OrderDetails", "Album_Id", c => c.Int());
            CreateIndex("dbo.OrderDetails", "Album_Id");
            AddForeignKey("dbo.OrderDetails", "Album_Id", "dbo.Albums", "Id");
        }
    }
}
