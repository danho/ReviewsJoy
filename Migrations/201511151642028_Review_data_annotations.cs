namespace ReviewsJoy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Review_data_annotations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reviews", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AlterColumn("dbo.Reviews", "ReviewText", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Reviews", "Author", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Author", c => c.String());
            AlterColumn("dbo.Reviews", "ReviewText", c => c.String());
            DropColumn("dbo.Reviews", "RowVersion");
            DropColumn("dbo.Reviews", "IsActive");
        }
    }
}
