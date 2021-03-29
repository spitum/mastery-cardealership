using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace GuildCars.UI.Models
{
    public class UserRoleProvider : RoleProvider
    {
        ApplicationDbContext _context;
        public UserRoleProvider()
        {
            _context = new ApplicationDbContext();
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {

            var roles = new List<string>();

            var userManager = new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>(_context));
            var roleManager = new RoleManager<IdentityRole, string>(new RoleStore<IdentityRole>(_context));

            var user = userManager.FindByName(username);
            if (user != null)
            {
                var role = roleManager.FindById(user.Roles.FirstOrDefault().RoleId).Name;

                roles.Add(role);
            }
            var rolesArray = roles.ToArray();

            return rolesArray;
            
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {

            var userManager = new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>(_context));
            var roleManager = new RoleManager<IdentityRole, string>(new RoleStore<IdentityRole>(_context));
            var user = userManager.FindByName(username);

            if(roleManager.FindById(user.Roles.FirstOrDefault().RoleId).Name == roleName)
                { return true; }
            return false; 
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}