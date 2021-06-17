namespace PAA_MVC_2021.Migrations
{
    using PAA_MVC_2021.Helpers;
    using PAA_MVC_2021.Models.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PAA_MVC_2021.Models.Entities.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new Role { RoleName = StringHelper.ROLE_ADMINISTRATOR });
                context.Roles.Add(new Role { RoleName = StringHelper.ROLE_USER });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var role = context.Roles.FirstOrDefault(x => x.RoleName == StringHelper.ROLE_ADMINISTRATOR);
                if (role != null)
                {
                    PasswordHelper.CreatePassword("123456", out byte[] passwordHash, out byte[] passwordSalt);
                    context.Users.Add(new User
                    {
                        CreatedAt = DateTime.Now,
                        EmailAddress = "leonardo.norambuena02@inacapmail.cl",
                        FirstName = "Leonardo",
                        LastName = "Norambuena",
                        RoleId = role.RoleId,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        VerifiedAt = DateTime.Now
                    });
                }
                
            }
        }
    }
}
