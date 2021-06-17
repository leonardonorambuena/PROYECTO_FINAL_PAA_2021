namespace PAA_MVC_2021.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(nullable: false),
                        ProductName = c.String(nullable: false),
                        ProductPrice = c.Int(nullable: false),
                        ProducStock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
