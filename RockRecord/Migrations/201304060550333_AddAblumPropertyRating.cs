namespace RockRecord.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAblumPropertyRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Rating", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Rating");
        }
    }
}
