using SampleArch.Model;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Admin
{
    public interface IModulesInRoleService : IEntityService<ModulesInRole>
    {
        List<ModulesInRolesViewModel> GetCartesian();
    }
}
