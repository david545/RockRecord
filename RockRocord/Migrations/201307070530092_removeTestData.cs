namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTestData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Girls", "Guy_Id", "dbo.Guys");
            DropIndex("dbo.Girls", new[] { "Guy_Id" });
            DropTable("dbo.Guys");
            DropTable("dbo.Girls");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Girls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Guy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Guys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Girls", "Guy_Id");
            AddForeignKey("dbo.Girls", "Guy_Id", "dbo.Guys", "Id", cascadeDelete: true);
        }
    }
}
