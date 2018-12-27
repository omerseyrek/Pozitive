using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SampleArch.Model.ViewModels;
using SampleArch.Service.Admin;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using SampleArch.Model;
using SampleArch.Repository.Admin;
using SampleArch.Repository;
using SampleArch.Modules;
using SampleArch.Model.Core;
using SampleArch.Service;

namespace Positive.Controllers
{
    [ModuleAuthorize("admin")]
    public class ModulesInRoleController : PositiveController<ModulesInRole, ModulesInRolesViewModel, IEntityService<ModulesInRole>>
    {
        IModulesInRoleService _mirService;
        public ModulesInRoleController(IModulesInRoleService mirService)
            : base("admin", mirService)
        {
            _mirService = mirService;
        }



        [View]
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {

            var modelLists = _mirService.GetCartesian();

            return Json(modelLists.ToDataSourceResult(request));
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request,  ModulesInRolesViewModel line, int Id = 0)
        {
            if (line != null && ModelState.IsValid)
            {
                 var userProfile =  ToolBox.GetUserProfile();

                 base.EditPost(line);

                 string cacheName = string.Format("{0}{1}", ApplicationConstants.ModuleCachePrefix, userProfile.Account);

                 UserManagementService.Cache.Remove(cacheName);

                 UserManagementService.GetAllowedModulesInRolesByUser();             
            }

            var result = Json(new[] { line }.ToDataSourceResult(request, ModelState));

            return result;
        }

  

        [Delete]
        public ActionResult Delete(int id)
        {
            return null;
        }
    }
}