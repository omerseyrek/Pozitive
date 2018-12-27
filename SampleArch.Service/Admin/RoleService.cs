using Newtonsoft.Json;
using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SampleArch.Service.Admin
{

    public class RoleService : EntityService<Role>, IRoleService
    {

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository theRepository, 
            IMultilanguageRepository mlRepository, IUILanguageRepository langRepository,
             ISessionManager sessionManager)
            : base(unitOfWork, theRepository, mlRepository, langRepository, sessionManager)
        {
        }



        IEnumerable<ValidationResult> IEntityService<Role>.GetBussinesValidations(BaseEntity role)
        {
            Role model = (Role)role;

            List<ValidationResult> validations = new List<ValidationResult>();

            bool exists = this.GetByFilter(p => p.Code == model.Code && p.Id != model.Id).Any();

            if (exists)
            {
                ValidationResult vr = new ValidationResult()
                {
                    MessType = MessageType.Error,
                    MemberName = "",
                    Message = Positive.Model.Languages.Admin.ValRoleExists
                };

                validations.Add(vr);
            };

            return validations;
        }

    }
}
