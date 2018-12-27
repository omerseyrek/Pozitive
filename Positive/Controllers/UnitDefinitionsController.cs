using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using SampleArch.Service.Admin;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleArch.Modules;
using SampleArch.Service;

namespace Positive.Controllers
{

        [ModuleAuthorize("stock")]
        public class UnitDefinitionsController : PositiveController<UnitDefinition, UnitDefinitionViewModel, IEntityService<UnitDefinition>>
        {
            IUnitDefinitionService _unitDefinitionService;
            public UnitDefinitionsController(IUnitDefinitionService unitDefinitionService)
                : base("stock", unitDefinitionService)
            {
                _unitDefinitionService = unitDefinitionService;
            }
            // GET: Category
            [View]
            public ActionResult Index()
            {
                return View();
            }

            public JsonResult List([DataSourceRequest] DataSourceRequest filter)
            {
                var lang = ToolBox.GetUserProfile().LanguageCode.ToUpper();

                var modelLists = _unitDefinitionService.ListByFilter(null);

                var results = modelLists.ToDataSourceResult(filter, s => new UnitDefinitionViewModel()
                {
                    Id = s.Id,
                    Code = s.Code,
                    Description = s.Description
                });

                return Json(results, JsonRequestBehavior.AllowGet);
            }


            [AllowAnonymous]
            [OutputCache(Duration = 100, VaryByParam = "none")]
            public ActionResult ListForCombo()
            {
                var data = _unitDefinitionService.GetAll().ToList();

                var results = data.Select(s => new UnitDefinitionViewModel
                {
                    Id = s.Id,
                    Code = s.Code,
                    Description = s.Code
                }).ToList();

                return Json(results, JsonRequestBehavior.AllowGet);
            }

    
            [Save]
            public ActionResult Edit(int id)
            {
                return base.EditGet(id, null);
            }


            [HttpPost]
            [Save]
            public ActionResult Edit(UnitDefinitionViewModel theModel)
            {
                return base.EditPost(theModel);
            }

           
            [HttpPost]
            [Delete]
            public ActionResult Delete(int id, UnitDefinition theModel)
            {
                return base.DeletePost(id);
            }
        }
    
}