namespace PAA_MVC_2021.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTokenToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserToken");
        }
    }
}
