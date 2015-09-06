namespace ReviewsJoy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class placeId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Locations", "placeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "placeId");
        }
    }
}
