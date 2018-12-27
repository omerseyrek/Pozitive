using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using SampleArch.Model;
using SampleArch.Model.ViewModels;
using SampleArch.Service.Admin;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SampleArch.Modules;
using SampleArch.Service;

namespace Positive.Controllers
{
   

    [ModuleAuthorize("admin")]
    public class RoleController : PositiveController<Role, RoleViewModel, IEntityService<Role>>
    {
        IRoleService _theService;
        public RoleController(IRoleService theService)
            : base("admin", theService)
        {
            _theService = theService;
        }



        [View]
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public JsonResult List([DataSourceRequest] DataSourceRequest filter)
        {
            var lang = ToolBox.GetUserProfile().LanguageCode.ToUpper();

            var modelLists = _theService.GetByFilter(null);

            var results = modelLists.ToDataSourceResult(filter, s => new RoleViewModel()
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description
            });

            return Json(results, JsonRequestBehavior.AllowGet);
            
        }


        [AllowAnonymous]
        public JsonResult ListForCombo()
        {
            var modelLists = _theService.GetAll();

            var results = modelLists.Select(s => new RoleViewModel
            {
                Id = s.Id,
                Code = s.Code,
                Description = s.Description
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
        public ActionResult Edit(RoleViewModel theModel)
        {
            return base.EditPost(theModel);
        }


        [Delete]
        public ActionResult Delete(int id)
        {
            return base.DeletePost(id);
        }
    }
    
}