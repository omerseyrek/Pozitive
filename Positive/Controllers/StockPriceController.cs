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
    [ModuleAuthorize("stock")]
    public class StockPriceController : PositiveController<StockPrice, StockPriceViewModel, IEntityService<StockPrice>>
    {
        IStockPriceService _theService;
        public StockPriceController(IStockPriceService theService)
            : base("Stock", theService)
        {
            _theService = theService;
        }


        [View]
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult List([DataSourceRequest] DataSourceRequest filter, int CatalogId = 0)
        {
            if (CatalogId > 0)
            {
                var dataList = _theService.GetByFilterWithInclude("StockItem", p => p.CatalogId == CatalogId);

                var results = dataList.ToDataSourceResult(filter, s => new StockPriceViewModel()
                {
                    Id = s.Id,
                    StockId = s.StockId,
                    SmartCode = s.StockItem.SmartCode,
                    StockName = s.StockItem.StockName,
                    SupplierProductCode = s.StockItem.SupplierProductCode,
                    Price = s.Price,
                });
                return Json(results, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new List<StockPriceViewModel>(), JsonRequestBehavior.AllowGet);
            }
        }



        [AllowAnonymous]
        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult ListForCombo()
        {
            var data = _theService.GetAll();

            var results = data.Select(s => new StockPriceViewModel
            {
                Id = s.Id,
                //bind other fields
            }).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }



        [Save]
        public ActionResult Edit(int id)
        {
            return base.EditGet(id, "Category");
        }


        [HttpPost]
        [Save]
        public ActionResult Edit(StockPriceViewModel viewModel)
        {
            return EditPost(viewModel);
        }



        [HttpPost]
        [Delete]
        public ActionResult Delete(int id)
        {
            return base.DeletePost(id);
        }



        [HttpPost]
        public ActionResult GridAdd([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] List<StockPriceViewModel> models)
        {
            if (models != null && ModelState.IsValid)
            {
                return Json(new[] { models[0] }.ToDataSourceResult(request, ModelState));
            }

            return Json(new[] { models }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult GridEdit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] List<StockPriceViewModel> models)
        {
            if (models != null && ModelState.IsValid)
            {
                return Json(new[] { models[0] }.ToDataSourceResult(request, ModelState));
            }

            return Json(new[] { models }.ToDataSourceResult(request, ModelState));
        }


        [HttpPost]
        public ActionResult GridDelete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] List<StockPriceViewModel> models)
        {
            if (models != null && ModelState.IsValid)
            {
                return Json(new[] { models[0] }.ToDataSourceResult(request, ModelState));
            }

            return Json(new[] { models }.ToDataSourceResult(request, ModelState));
        }

    }
}
