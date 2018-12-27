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

namespace SampleArch.Service.Admin
{

    public class UserTitleService : EntityService<UserTitle>, IUserTitleService
    {

        public UserTitleService(IUnitOfWork unitOfWork,
            IUserTitleRepository theRepository,
            IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, theRepository, mlRepository, langRepository, sessionManager)
        {

        }

        IEnumerable<ValidationResult> IEntityService<UserTitle>.GetBussinesValidations(BaseEntity userTitle)  
        {
            UserTitle model = (UserTitle)userTitle;

            List<ValidationResult> validations = new List<ValidationResult>();

            bool exists = this.GetByFilter(p => p.Code == model.Code && p.Id != model.Id).Any();

            if (exists)
            {
                ValidationResult vr = new ValidationResult()
                {
                    MessType = MessageType.Error,
                    MemberName = "Code",
                    Message = Positive.Model.Languages.Admin.ValTitleExists
                };

                validations.Add(vr);
            };

            return validations;
        }
    }
}
