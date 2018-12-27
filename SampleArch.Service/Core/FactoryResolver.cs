using SampleArch.Model.Core;
using SampleArch.Repository.Admin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Core
{
    public class FactoryResolver
    {
        public static IUserRepository GetUserRepository(DbContext context) 
        {
            return new UserRepository(context);
        }

        public static IRoleRepository GetRoleRepository(DbContext context)
        {
            return new RoleRepository(context, new MVCSessionManager());
        }

        public static IMenuRepository GetMenuRepository(DbContext context)
        {
            return new MenuRepository(context);
        }

        public static IModuleRepository GetModuleRepository(DbContext context)
        {
            return new ModuleRepository(context);
        }

        public static IModulesInRoleRepository GetModulesInRolesRepository(DbContext context)
        {
            ISessionManager sessionManeger = new MVCSessionManager();
            return new ModulesInRoleRepository(context, sessionManeger);
        }

        public static IExceptionManager GetExceptionManager() 
        {
            return new ExceptionManeger();
        }
    }
}
