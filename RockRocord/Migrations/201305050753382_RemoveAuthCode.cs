namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAuthCode : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Members", "AuchCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "AuchCode", c => c.String(maxLength: 36));
        }
    }
}
