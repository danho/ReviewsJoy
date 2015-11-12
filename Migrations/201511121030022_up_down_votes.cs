namespace ReviewsJoy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up_down_votes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "UpVotes", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "DownVotes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "DownVotes");
            DropColumn("dbo.Reviews", "UpVotes");
        }
    }
}
