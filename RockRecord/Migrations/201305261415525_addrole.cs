namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Role");
        }
    }
}
