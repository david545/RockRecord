namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTestModel3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Girls", "Guy_Id", "dbo.Guys");
            DropIndex("dbo.Girls", new[] { "Guy_Id" });
            AlterColumn("dbo.Girls", "Guy_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.Girls", "Guy_Id", "dbo.Guys", "Id", cascadeDelete: true);
            CreateIndex("dbo.Girls", "Guy_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Girls", new[] { "Guy_Id" });
            DropForeignKey("dbo.Girls", "Guy_Id", "dbo.Guys");
            AlterColumn("dbo.Girls", "Guy_Id", c => c.Int());
            CreateIndex("dbo.Girls", "Guy_Id");
            AddForeignKey("dbo.Girls", "Guy_Id", "dbo.Guys", "Id");
        }
    }
}
