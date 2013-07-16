namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBiography : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Biography", c => c.String(maxLength: 500));
            DropColumn("dbo.Members", "NickName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "NickName", c => c.String(nullable: false, maxLength: 25));
            DropColumn("dbo.Members", "Biography");
        }
    }
}
