namespace PAA_MVC_2021.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameInUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserAddress", c => c.String());
            DropColumn("dbo.Users", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Address", c => c.String());
            DropColumn("dbo.Users", "UserAddress");
        }
    }
}
