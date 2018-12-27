using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using SampleArch.Service.Admin;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Admin
{

    public class UsersInRoleService : EntityService<UsersInRole>, IUsersInRoleService
    {
        public UsersInRoleService(IUnitOfWork unitOfWork,
            IUsersInRoleRepository theRepository,
            IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, theRepository, mlRepository, langRepository, sessionManager)
        {
        }


        IEnumerable<ValidationResult> IEntityService<UsersInRole>.GetBussinesValidations(BaseEntity userTitle)
        {
            UsersInRole model = (UsersInRole)userTitle;

            List<ValidationResult> validations = new List<ValidationResult>();

            bool exists = this.GetByFilter(p => p.UserID == model.UserID && p.RoleId == model.RoleId && p.Id != model.Id).Any();

            if (exists)
            {
                ValidationResult vr = new ValidationResult()
                {
                    MessType = MessageType.Error,
                    MemberName = "",
                    Message = Positive.Model.Languages.Admin.ValUserInRoleExists
                };

                validations.Add(vr);
            };

            return validations;
        }
    }
}
