using GuildCars.Models.Tables;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.EF
{
    public class UsersRepositoryEF
    {
        public List<UserRoleInfo> GetUsers()
        {
            var repo = new GuildCarsEntities();

            var listOfUsers = (from u in repo.AspNetUsers
                               let query = (from ur in repo.Set<IdentityUserRole>()
                                            where ur.UserId.Equals(u.Id)
                                            join r in repo.AspNetRoles on ur.RoleId equals r.Id
                                            select r.Name)
                               select new UserRoleInfo() { User = u, Roles = query.ToList<string>() })
                         .ToList();
            return listOfUsers;
        }
    }
}
