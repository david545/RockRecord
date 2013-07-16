namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTestModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Girls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guys", t => t.Guy_Id)
                .Index(t => t.Guy_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Girls", new[] { "Guy_Id" });
            DropForeignKey("dbo.Girls", "Guy_Id", "dbo.Guys");
            DropTable("dbo.Girls");
            DropTable("dbo.Guys");
        }
    }
}
