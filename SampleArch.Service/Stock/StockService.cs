using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using SampleArch.Repository.CRM;
using SampleArch.Repository.Stock;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Stock
{

    public class StockService : EntityService<SampleArch.Model.Models.Stock>, IStockService
    {
        ICategoryRepository _categoryRepository;

        public StockService(IUnitOfWork unitOfWork,
            IStockRepository stockRepository, 
            ISessionManager sessionMan, 
            ICategoryRepository categoryRepository,
            IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, stockRepository, mlRepository, langRepository, sessionManager)
        {
            _categoryRepository = categoryRepository;
        }


        IEnumerable<ValidationResult> IEntityService<SampleArch.Model.Models.Stock>.GetBussinesValidations(BaseEntity data)
        {
            SampleArch.Model.Models.Stock model = (SampleArch.Model.Models.Stock)data;

            List<ValidationResult> validations = new List<ValidationResult>();

            var stock = base.TheRepository.FindBy(p => p.Id != model.Id && p.SupplierId == model.SupplierId && p.SmartCode == model.SmartCode).FirstOrDefault<SampleArch.Model.Models.Stock>();
            if (stock != null)
            {
                validations.Add(new ValidationResult()
                {
                    MemberName = "SmartCode",
                    MessType = Model.Core.MessageType.Error,
                    Message = Positive.Model.Languages.Stock.ValFirmSmartCodeExists
                });
            }

            stock = base.TheRepository.FindBy(p => p.Id != model.Id && p.SupplierId == model.SupplierId && p.SupplierProductCode == model.SupplierProductCode).FirstOrDefault<SampleArch.Model.Models.Stock>();
            if (stock != null)
            {
                validations.Add(new ValidationResult()
                {
                    MemberName = "SmartCode",
                    MessType = Model.Core.MessageType.Error,
                    Message = Positive.Model.Languages.Stock.ValFirmProductCode
                });
            }

            return validations;
        }

    }
}
