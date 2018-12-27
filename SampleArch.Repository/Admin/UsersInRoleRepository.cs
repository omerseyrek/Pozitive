using SampleArch.Model;
using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Admin
{

    public class UsersInRoleRepository : GenericRepository<UsersInRole>, IUsersInRoleRepository
    {
        public UsersInRoleRepository(DbContext context)
            : base(context)
        {
        }

    }
}
