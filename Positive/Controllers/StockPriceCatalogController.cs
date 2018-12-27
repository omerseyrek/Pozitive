using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using SampleArch.Model.Models;
using SampleArch.Service.Admin;
using SampleArch.Service.Core;
using SampleArch.Service.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleArch.Model.ViewModels;
using System.Threading;
using SampleArch.Modules;
using SampleArch.Model.Core;
using SampleArch.Service;
using SampleArch.Service.Stock;

namespace Positive.Controllers
{
    [ModuleAuthorize("Stock")]
    public class StockPriceCatalogController : PositiveController<StockPriceCatalog, StockPriceCatalogViewModel, IEntityService<StockPriceCatalog>>
    {
        IStockPriceCatalogService _theService;
        public StockPriceCatalogController(IStockPriceCatalogService theService)
            : base("Stock", theService)
        {
            _theService = theService;
        }
      

        [View]
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult List([DataSourceRequest] DataSourceRequest filter)
        {
            var dataList = _theService.GetAllWithIncludings("SupplierCompany");

            var results = dataList.ToDataSourceResult(filter, s => new StockPriceCatalogViewModel()
            {
                Id = s.Id,
                CatalogCode = s.CatalogCode,
                Definition = s.Definition,
                SupplierId = s.SupplierId,
                SupplierName = s.SupplierCompany.FirmName
            });
            return Json(results, JsonRequestBehavior.AllowGet);
        }



        [AllowAnonymous]
        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult ListForCombo()
        {
            var data = _theService.GetAll();

            var results = data.Select(s => new StockPriceCatalogViewModel
            {
                Id = s.Id,
                CatalogCode = s.CatalogCode
            }).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
       

   
        [Save]
        public ActionResult Edit(int id)
        {
            return base.EditGet(id, "Company");
        }

        
        [HttpPost]
        [Save]
        public ActionResult Edit(StockPriceCatalogViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userProfile = ToolBox.GetUserProfile();

                    var sublist = Request.Form["subGridItems"];

                    var destroyed = Request.Form["subGridDestroyedItems"];

                    viewModel.StockPriceViewModels = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockPriceViewModel>>(sublist);

                    viewModel.DestroyedIDs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(destroyed);

                    if (viewModel.Id > 0)
                    {
                        _theService.UpdateWithLines(viewModel);
                    }
                    else
                    {
                        _theService.CreateWithLines(viewModel);
                    }
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

     
     
        [HttpPost]
        [Delete]
        public ActionResult Delete(int id)
        {
            return base.DeletePost(id);
        }

    }
}