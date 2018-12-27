using Kendo.Mvc.UI;
using SampleArch.Service.Core;
using SampleArch.Service.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using SampleArch.Model.ViewModels;
using SampleArch.Model.Models;
using System.Threading;
using SampleArch.Model.Core;
using SampleArch.Model;
using System.Data.Entity.Validation;
using SampleArch.Modules;
using SampleArch.Service;
using System.IO;

namespace Positive.Controllers
{
    [ModuleAuthorize("Stock")]
    public class StockController : PositiveController<Stock, StockViewModel, IEntityService<Stock>>
    {
        IStockService _stockService;
        ICategoryService _categoryService;

        public StockController(IStockService stockService, ICategoryService categoryService)
            : base("stock", stockService)
        {
            _stockService = stockService;
            _categoryService = categoryService;
            Thread.CurrentThread.CurrentCulture = ToolBox.GetUserProfileCulture();
            Thread.CurrentThread.CurrentUICulture = ToolBox.GetUserProfileCulture();
        }

        [AllowAnonymous]
        public JsonResult ListForSearch([DataSourceRequest] DataSourceRequest request, int SupplierId = 0)
        {
            var data = _stockService.GetByFilterWithInclude("UnitDefinition",  p =>  SupplierId == 0 || p.SupplierId == SupplierId);
            
            var results = data.Select(s => new StockViewModel
            {
                Id = s.Id,
                SmartCode = s.SmartCode,
                StockName = s.StockName,
                SupplierId = s.SupplierId,
                SupplierProductCode = s.SupplierProductCode,
                StockUnitId = s.StockUnitId,
                UnitName = s.UnitDefinition.Code
            }).ToList();

            var res = results.ToDataSourceResult(request);

            var tosend = Json(results, JsonRequestBehavior.AllowGet);

            return tosend;
        }

        [AllowAnonymous]
        public JsonResult ListForSearchSmartCode([DataSourceRequest] DataSourceRequest request)
        {
            var data = _stockService.GetByFilterWithInclude("UnitDefinition",null);

            var results = data.Select(s => new StockViewModel
            {
                SmartCode = s.SmartCode,
                StockUnitId = s.StockUnitId,
                UnitName = s.UnitDefinition.Code
            }).Distinct().ToList();

            var res = results.ToDataSourceResult(request);

            var tosend = Json(results, JsonRequestBehavior.AllowGet);

            return tosend;
        }

        [AllowAnonymous]
        public JsonResult ListForSearchSupplierCode([DataSourceRequest] DataSourceRequest request)
        {
            var data = _stockService.GetByFilterWithInclude("UnitDefinition", null);

            var results = data.Select(s => new StockViewModel
            {
                SupplierProductCode = s.SupplierProductCode,
                StockUnitId = s.StockUnitId,
                UnitName = s.UnitDefinition.Code
            }).Distinct().ToList();

            var res = results.ToDataSourceResult(request);

            var tosend = Json(results, JsonRequestBehavior.AllowGet);

            return tosend;
        }

        [View]
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult List([DataSourceRequest] DataSourceRequest filter)
        {
            var dataList = _stockService.GetAll().ToList();

            var results = dataList.ToDataSourceResult(filter, s => new StockViewModel()
            {
                Id = s.Id,
                CategoryId = s.CategoryId,
                CodeIndex = s.CodeIndex,
                SmartCode = s.SmartCode,
                StockName = s.StockName,
                Definition = s.Definition,
                SupplierName = s.SupplierCompany.FirmName,
                SupplierId = s.SupplierId,
                SupplierProductCode = s.SupplierProductCode,
                UnitName = s.UnitDefinition.Code
            });
            return Json(results, JsonRequestBehavior.AllowGet);
        }


        [Save]
        public ActionResult AddToCategory(int Id = 0)
        {
            var category = _categoryService.Get(Id);

            StockViewModel model = new StockViewModel()
            {
                CatgoryCode = category.CompleteKey,
                SmartCode = category.CompleteKey,
                StockName = "",
                SupplierProductCode = "",
                ViewMode = EnumDefinitions.StockViewMode.AddToCategory,
                Category = new Category()
                {
                    Id = category.Id,
                    Key = category.Key,
                    CompleteKey = category.CompleteKey,
                    Description = category.Description,
                    Name = category.Name
                }
            };

            return View("Edit", model);
        }


        [Save]
        [HttpPost]
        public ActionResult AddToCategory(StockViewModel viewModel)
        {
            viewModel.Id = 0;
            return Edit(viewModel, null);
        }



        [View]
        public ActionResult Edit(int id)
        {
            Stock dataEntity = new Stock();
            if (id > 0)
            {
                dataEntity = _stockService.GetWithIncludings(id, "StockImages");
            }

            StockViewModel model = new StockViewModel()
            {
                SmartCode = "",
                StockName = "",
                SupplierProductCode = "",
                Category = new Category()
                {
                    CompleteKey = "",
                    Description = "",
                    Name = ""
                }
            };

            if (id > 0)
            {
                model.Id = dataEntity.Id;
                model.CategoryId = dataEntity.CategoryId;
                model.CatgoryCode = dataEntity.Category.CompleteKey;
                model.CodeIndex = dataEntity.CodeIndex;
                model.SmartCode = dataEntity.SmartCode;
                model.StockName = dataEntity.StockName;
                model.Definition = dataEntity.Definition;
                model.SupplierId = dataEntity.SupplierId;
                model.SupplierProductCode = dataEntity.SupplierProductCode;
                model.StockUnitId = dataEntity.StockUnitId;
                model.Category = dataEntity.Category;

                if (dataEntity.StockImages.Count > 0)
                {
                    model.SmallImageBinary = dataEntity.StockImages.First().StockImageSmall;

                    model.LargeImageBinary = dataEntity.StockImages.First().StockImageLarge;
                }

                model.ViewMode = EnumDefinitions.StockViewMode.Update;
            }
            else
            {
                model.ViewMode = EnumDefinitions.StockViewMode.Add;
            }

            return View(model);
        }


        [HttpPost]
        [Save]
        public ActionResult Edit(StockViewModel viewModel, IEnumerable<HttpPostedFileBase> StockImages)
        {
            var category = _categoryService.GetByFilter(p => p.CompleteKey == viewModel.CatgoryCode).FirstOrDefault<Category>();

            if (category == null)
            {
                List<ValidationResult> BussinesValidations = new List<ValidationResult>();
                BussinesValidations.Add(new ValidationResult()
                {
                    MemberName = "CatgoryCode",
                    MessType = MessageType.Error,
                    Message = Positive.Model.Languages.Stock.ValStockCategory
                });

                PositiveResults pr = new PositiveResults();

                pr.Success = false;

                pr.AddResultsRange(BussinesValidations);

                return  Json(pr, JsonRequestBehavior.AllowGet);
            }

            

            if (viewModel.SmallImage != null || viewModel.LargeImage != null)
            {
                viewModel.StockImages = new List<StockImage>();

                var image = new StockImage();
                
                if(viewModel.SmallImage  != null)
                {
                    using (var reader = new BinaryReader(viewModel.SmallImage.InputStream))
                    {
                        image.StockImageSmall = reader.ReadBytes(viewModel.SmallImage.ContentLength);
                    }
                }

                if(viewModel.LargeImage  != null)
                {
                    using (var reader = new BinaryReader(viewModel.LargeImage.InputStream))
                    {
                        image.StockImageLarge = reader.ReadBytes(viewModel.LargeImage.ContentLength);
                    }
                }

                image.StockImageLarge = image.StockImageSmall;

                var userProfile = ToolBox.GetUserProfile();
                PocoHelper.SetTractionFieldsOfEntitiy(image, Int32.Parse(userProfile.UserId), DateTime.Now);

                viewModel.StockImages.Add(image);
            }


            viewModel.CategoryId = category.Id;

            viewModel.SmartCode = viewModel.CatgoryCode + "." + viewModel.CodeIndex;

            return base.EditPost(viewModel);
        }


        [HttpPost]
        [Delete]
        public ActionResult Delete(int id)
        {
            return base.DeletePost(id);
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            return null;


        }

        [HttpPost]
        public ActionResult UploadImageSmall()
        {
            return null;

        }
 
    }

}