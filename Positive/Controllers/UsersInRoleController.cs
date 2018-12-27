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
using SampleArch.Model;

namespace Positive.Controllers
{
    [ModuleAuthorize("Admin")]
    public class UsersInRoleController : PositiveController<UsersInRole, UsersInRoleViewModel, IEntityService<UsersInRole>>
    {
        IUsersInRoleService _theService;
        public UsersInRoleController(IUsersInRoleService theService)
            : base("Admin", theService)
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
            var dataList = _theService.GetAll().ToList();

            var results = dataList.ToDataSourceResult(filter, s => new UsersInRoleViewModel()
            {
                Id = s.Id,
                RoleId = s.RoleId,
                UserID = s.UserID,
                Account = s.Users.Account,
                RoleCode = s.Roles.Code,
                RoleName = s.Roles.Code
            });
            return Json(results, JsonRequestBehavior.AllowGet);
        }

   
        [Save]
        public ActionResult Edit(int id)
        {
            return base.EditGet(id, null);
        }

        
        [HttpPost]
        [Save]
        public ActionResult Edit(UsersInRoleViewModel viewModel)
        {
            return EditPost(viewModel);
        }

     
     
        [HttpPost]
        [Delete]
        public ActionResult Delete(int id)
        {
            return base.DeletePost(id);
        }
    }
}