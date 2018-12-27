using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.ViewModels;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Admin
{
    public class ModulesInRoleService : EntityService<ModulesInRole>, IModulesInRoleService
    {
        IUsersInRoleRepository _uirRepository;

        public ModulesInRoleService(IUnitOfWork unitOfWork, 
            IModulesInRoleRepository theRepository,
            IUsersInRoleRepository uirRepository,
            IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, theRepository, mlRepository, langRepository, sessionManager)
        {
            _uirRepository = uirRepository;
        }

        public List<ModulesInRolesViewModel> GetCartesian()
        {
            return (base.TheRepository as IModulesInRoleRepository).GetCartesian();
        }


        public ModulesInRolesViewModel GetModuleAuthorizeOfUser(int UserId, string ModuleName)
        {
            ModulesInRolesViewModel theMergedAuthorisation = new ModulesInRolesViewModel();

            List<UsersInRole> userRoles = _uirRepository.GetAll().Where(p => p.UserID == UserId).ToList<UsersInRole>();

            return null;
        }

        public ModulesInRolesViewModel GetModuleAuthorizeOfRole(string RoleName, string ModuleName)
        {
            throw new NotImplementedException();
        }
    }
}
