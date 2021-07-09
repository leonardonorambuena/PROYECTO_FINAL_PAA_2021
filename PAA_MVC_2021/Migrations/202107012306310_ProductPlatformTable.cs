namespace PAA_MVC_2021.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductPlatformTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductPlatforms",
                c => new
                    {
                        ProductPlatformId = c.Int(nullable: false, identity: true),
                        ProductPlatformName = c.String(nullable: false),
                        ProductPlatformLogo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductPlatformId);
            
            AddColumn("dbo.Products", "PlatformId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Address", c => c.String());
            CreateIndex("dbo.Products", "PlatformId");
            AddForeignKey("dbo.Products", "PlatformId", "dbo.ProductPlatforms", "ProductPlatformId", cascadeDelete: true);
            DropColumn("dbo.Users", "UserAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserAddress", c => c.String());
            DropForeignKey("dbo.Products", "PlatformId", "dbo.ProductPlatforms");
            DropIndex("dbo.Products", new[] { "PlatformId" });
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Products", "PlatformId");
            DropTable("dbo.ProductPlatforms");
        }
    }
}
