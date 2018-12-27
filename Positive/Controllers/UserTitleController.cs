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
using Newtonsoft.Json;

namespace Positive.Controllers
{
    [ModuleAuthorize("admin")]
    public class UserTitleController : PositiveController<UserTitle, UserTitleViewModel, IEntityService<UserTitle>>
    {
        public UserTitleController(IUserTitleService theService)
            : base("admin", theService)
        {
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

            var modelLists = TheService.GetByFilter(null);

            var results = modelLists.ToDataSourceResult(filter, s => new UserTitleViewModel()
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
            var lang = ToolBox.GetUserProfile().LanguageCode.ToUpper();

            var modelLists = TheService.GetByFilter(null);

            var results = modelLists.Select(s => new UserTitleViewModel
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
        public ActionResult Edit(UserTitleViewModel theModel)
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