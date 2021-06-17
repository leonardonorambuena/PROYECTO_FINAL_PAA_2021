namespace PAA_MVC_2021.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableUsersAndRoles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        EmailAddress = c.String(nullable: false, maxLength: 120),
                        PasswordHash = c.Binary(nullable: false),
                        PasswordSalt = c.Binary(nullable: false),
                        PhoneNumber = c.String(),
                        Address = c.String(),
                        VerifiedAt = c.DateTime(),
                        RoleId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
