namespace PAA_MVC_2021.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfirmedAddCanBeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sales", "ConfirmedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sales", "ConfirmedAt", c => c.DateTime(nullable: false));
        }
    }
}
