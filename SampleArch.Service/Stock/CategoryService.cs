using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using SampleArch.Repository.CRM;
using SampleArch.Repository.Stock;
using SampleArch.Service.Core;
using SampleArch.Service.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.CRM
{

    public class CategoryService : EntityService<Category>, ICategoryService
    {
       
        public CategoryService(IUnitOfWork unitOfWork,
            ICategoryRepository theRepository,
            IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, theRepository, mlRepository, langRepository, sessionManager)
        {
        }

        public IEnumerable<Category> ListForSearch()
        {
            return (base.TheRepository as ICategoryRepository).ListForSearch();
        }

        IEnumerable<ValidationResult> IEntityService<Category>.GetBussinesValidations(BaseEntity category)
        {
            Category model = (Category)category;

            List<ValidationResult> validations = new List<ValidationResult>();

            bool exists = this.GetByFilter(p => p.CompleteKey == model.CompleteKey && p.Id != model.Id).Any();

            if (exists)
            {
                ValidationResult vr = new ValidationResult()
                {
                    MessType = MessageType.Error,
                    MemberName = "Key",
                    Message = Positive.Model.Languages.Stock.ValCategoryExists
                };

                validations.Add(vr);
            };

            return validations;
        }
    }
}
