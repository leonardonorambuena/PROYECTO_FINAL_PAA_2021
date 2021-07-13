namespace PAA_MVC_2021.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSaleId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SaleLines", "Sale_SaleId", "dbo.Sales");
            DropIndex("dbo.SaleLines", new[] { "Sale_SaleId" });
            RenameColumn(table: "dbo.SaleLines", name: "Sale_SaleId", newName: "SaleId");
            AlterColumn("dbo.SaleLines", "SaleId", c => c.Int(nullable: false));
            CreateIndex("dbo.SaleLines", "SaleId");
            AddForeignKey("dbo.SaleLines", "SaleId", "dbo.Sales", "SaleId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleLines", "SaleId", "dbo.Sales");
            DropIndex("dbo.SaleLines", new[] { "SaleId" });
            AlterColumn("dbo.SaleLines", "SaleId", c => c.Int());
            RenameColumn(table: "dbo.SaleLines", name: "SaleId", newName: "Sale_SaleId");
            CreateIndex("dbo.SaleLines", "Sale_SaleId");
            AddForeignKey("dbo.SaleLines", "Sale_SaleId", "dbo.Sales", "SaleId");
        }
    }
}
