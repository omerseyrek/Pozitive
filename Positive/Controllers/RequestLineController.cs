using Kendo.Mvc.UI;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using SampleArch.Modules;
using SampleArch.Service;
using SampleArch.Service.Core;
using SampleArch.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Positive.Controllers
{


    [ModuleAuthorize("pre")]
    public class RequestLineController : PositiveController<RequestLine, RequestLineViewModel, IEntityService<RequestLine>>
    {
        IRequestLineService _theService;
        public RequestLineController(IRequestLineService theService)
            : base("pre", theService)
        {
            _theService = theService;
        }


        [View]
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult List([DataSourceRequest] DataSourceRequest filter, int PreRequestId = 0)
        {
            if (PreRequestId > 0)
            {
                var dataList = _theService.GetByFilterWithInclude("UnitDefinition", p => p.PreRequestId == PreRequestId);

                var results = dataList.ToDataSourceResult(filter, s => new RequestLineViewModel()
                {
                    Id = s.Id,
                    SmartCode = s.SmartCode,
                    SupplierProductCode = s.SupplierProductCode,
                    Description = s.Description,
                    Quantity = s.Quantity,
                    StockUnitId = s.StockUnitId,
                    StockUnitCode = s.UnitDefinition.Code
                });
                return Json(results, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new List<RequestLineViewModel>(), JsonRequestBehavior.AllowGet);
            }
        }



        [AllowAnonymous]
        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult ListForCombo()
        {
            var data = _theService.GetAll();

            var results = data.Select(s => new RequestLineViewModel
            {
                Id = s.Id,
                //bind other fields
            }).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }



        [Save]
        public ActionResult Edit(int id)
        {
            return base.EditGet(id, "UnitDefinition");
        }


        [HttpPost]
        [Save]
        public ActionResult Edit(RequestLineViewModel viewModel)
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
        public ActionResult GridAdd([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] List<RequestLineViewModel> models)
        {
            if (models != null && ModelState.IsValid)
            {
                return Json(new[] { models[0] }.ToDataSourceResult(request, ModelState));
            }

            return Json(new[] { models }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult GridEdit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] List<RequestLineViewModel> models)
        {
            if (models != null && ModelState.IsValid)
            {
                return Json(new[] { models[0] }.ToDataSourceResult(request, ModelState));
            }

            return Json(new[] { models }.ToDataSourceResult(request, ModelState));
        }


        [HttpPost]
        public ActionResult GridDelete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] List<RequestLineViewModel> models)
        {
            if (models != null && ModelState.IsValid)
            {
                return Json(new[] { models[0] }.ToDataSourceResult(request, ModelState));
            }

            return Json(new[] { models }.ToDataSourceResult(request, ModelState));
        }
    }
}