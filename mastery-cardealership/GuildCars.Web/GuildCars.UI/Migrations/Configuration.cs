namespace GuildCars.UI.Migrations
{
    using GuildCars.UI.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCars.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCars.UI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup create  Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "spitum";
                user.Email = "spitum@gmail.com";

                string userPWD = "test123!";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Manager role     
            if (!roleManager.RoleExists("Sales"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Sales";
                roleManager.Create(role);

            }

            // creating Creating Employee role     
            if (!roleManager.RoleExists("Disabled"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Disabled";
                roleManager.Create(role);

            }
        }
    }
    
}
