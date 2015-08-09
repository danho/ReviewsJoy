namespace ReviewsJoy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryAndLocation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewId = c.Int(nullable: false, identity: true),
                        ReviewText = c.String(),
                        Author = c.String(),
                        Stars = c.Int(nullable: false),
                        Category_CategoryId = c.Int(),
                        Location_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Location_LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Reviews", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Reviews", new[] { "Location_LocationId" });
            DropIndex("dbo.Reviews", new[] { "Category_CategoryId" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Categories");
        }
    }
}
