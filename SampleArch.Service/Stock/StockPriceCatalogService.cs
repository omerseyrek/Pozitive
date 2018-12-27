using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
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

    public class StockPriceCatalogService : EntityService<StockPriceCatalog>, IStockPriceCatalogService
    {
        IStockPriceRepository _stockPriceRepository;
        ICategoryRepository _categoryRepository;
        public StockPriceCatalogService(IUnitOfWork unitOfWork,
            IStockPriceCatalogRepository stockPriceCatalogRepository,
            IStockPriceRepository stockPriceRepository, 
            ISessionManager sessionMan, 
            ICategoryRepository categoryRepository,
            IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, stockPriceCatalogRepository, mlRepository, langRepository, sessionManager)
        {
            _stockPriceRepository = stockPriceRepository;
            _categoryRepository = categoryRepository;
        }




        public void CreateWithLines(StockPriceCatalogViewModel model)
        {
            UserProfileViewModel user =  SessionManager.GetUserProfile();

            StockPriceCatalog data = new StockPriceCatalog();

            if (model.Id > 0)
            {
                data = TheRepository.Get(model.Id);
            }

            PocoHelper.UpdatePocoMapper<StockPriceCatalogViewModel, StockPriceCatalog>(model, data);

            PocoHelper.SetTractionFieldsOfEntitiy(data, Int32.Parse(user.UserId), DateTime.Now);

            data.Prices = new List<StockPrice>();

            foreach (StockPriceViewModel item in model.StockPriceViewModels)
            {
                StockPrice line = new StockPrice();

                PocoHelper.UpdatePocoMapper<StockPriceViewModel, StockPrice>(item, line);

                PocoHelper.SetTractionFieldsOfEntitiy(line, Int32.Parse(user.UserId), DateTime.Now);

                data.Prices.Add(line);
            }

            TheRepository.Add(data);

            UnitOfWork.Commit();

            if (HasEntityMultilanguageField())
            {
                base.CreateMultilanguageDefinitions(data);
                UnitOfWork.Commit(); 
            }
        }

        public void DeleteWithLines(Model.ViewModels.StockPriceCatalogViewModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateWithLines(Model.ViewModels.StockPriceCatalogViewModel model)
        {
            UserProfileViewModel user = SessionManager.GetUserProfile();

            StockPriceCatalog data = new StockPriceCatalog();

            if (model.Id > 0)
            {
                data = TheRepository.GetWithInclude( model.Id, "Prices");
            }

            PocoHelper.UpdatePocoMapper<StockPriceCatalogViewModel, StockPriceCatalog>(model, data);

            PocoHelper.SetTractionFieldsOfEntitiy(data, Int32.Parse(user.UserId), DateTime.Now);

            if (data.Prices == null)
            {
                data.Prices = new List<StockPrice>();
            }
         
            foreach (StockPriceViewModel item in model.StockPriceViewModels)
            {
                StockPrice line = new StockPrice();
          
                if (item.Id == 0)
                {
                    PocoHelper.UpdatePocoMapper<StockPriceViewModel, StockPrice>(item, line);
                    PocoHelper.SetTractionFieldsOfEntitiy(line, Int32.Parse(user.UserId), DateTime.Now);
                    line.CatalogId = data.Id;
                    data.Prices.Add(line);
                }
                else
                {
                    line = data.Prices.FirstOrDefault(p => p.Id == item.Id);
                    PocoHelper.UpdatePocoMapper<StockPriceViewModel, StockPrice>(item, line);
                    PocoHelper.SetTractionFieldsOfEntitiy(line, Int32.Parse(user.UserId), DateTime.Now);
                    line.CatalogId = data.Id;
                }
            }

            foreach (int toDestroyID in model.DestroyedIDs)
            {
                StockPrice line = new StockPrice();
                if (toDestroyID > 0)
                {
                    line = data.Prices.FirstOrDefault(p => p.Id == toDestroyID);

                    _stockPriceRepository.Delete(line);
                }
            }

            TheRepository.Edit(data); 

            UnitOfWork.Commit();

            if (HasEntityMultilanguageField())
            {
                base.CreateMultilanguageDefinitions(data);
                UnitOfWork.Commit();
            }
        }

        IEnumerable<ValidationResult> IEntityService<StockPriceCatalog>.GetBussinesValidations(BaseEntity data)
        {
            StockPriceCatalog model = (StockPriceCatalog)data;

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
