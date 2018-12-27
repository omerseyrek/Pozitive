using SampleArch.Model;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleArch.Service.Admin
{
    public interface IUserManagementService
    {
        bool ValidateUser(string username, string password);
        User GetUserByName(string username);
        ModulesInRolesViewModel GetAllowedRolesOfUserByModuleCode(string ModuleCode);
    }
}
