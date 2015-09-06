namespace ReviewsJoy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class placeId1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Locations", "placeId");
            AddColumn("dbo.Locations", "placeId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Locations", "placeId");
        }
    }
}
