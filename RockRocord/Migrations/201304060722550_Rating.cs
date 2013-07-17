namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rating : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Rating", c => c.Single(nullable: false));
        }
    }
}
