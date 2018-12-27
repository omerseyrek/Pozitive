using SampleArch.Model;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Admin
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        List<Role> GetRolesOfUser(int userId);
    }
}
