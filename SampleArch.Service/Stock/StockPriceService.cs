using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using SampleArch.Repository.Stock;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Stock
{

        public class StockPriceService : EntityService<StockPrice>, IStockPriceService
    {
        ICategoryRepository _categoryRepository;
        public StockPriceService(IUnitOfWork unitOfWork,
            IStockPriceRepository stockPriceRepository, 
            ISessionManager sessionMan, 
            ICategoryRepository categoryRepository,
            IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, stockPriceRepository, mlRepository, langRepository, sessionManager)
        {
            _categoryRepository = categoryRepository;
        }


        IEnumerable<ValidationResult> IEntityService<StockPrice>.GetBussinesValidations(BaseEntity data)
        {
            StockPrice model = (StockPrice)data;

            List<ValidationResult> validations = new List<ValidationResult>();

            bool exists = false;  // to do : do bussines validation implementations

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

