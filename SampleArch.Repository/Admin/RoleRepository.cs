using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Admin
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        ISessionManager _sessionManeger;
        public RoleRepository(DbContext context, ISessionManager sessionManeger)
            : base(context)
        {
            _sessionManeger = sessionManeger;
        }

        public List<Role> GetRolesOfUser(int userId)
        {
            return this.FindBy(p => p.UsersInRoles.Any(r => r.UserID == userId)).ToList();
        }
    }
}
