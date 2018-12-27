using SampleArch.Model;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleArch.Repository.Admin
{
    public interface IModulesInRoleRepository : IGenericRepository<ModulesInRole>
    {
        List<ModulesInRolesViewModel> GetCartesian();
    }
}
