using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using SampleArch.Model.ViewModels;
using SampleArch.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SampleArch.Model;
using System.Globalization;
using System.Threading;
using SampleArch.Modules;
using SampleArch.Service;
using SampleArch.Service.Core;

namespace Positive.Controllers
{
    [ModuleAuthorize("admin")]
    public class UserController : PositiveController<User, UserViewModel, IEntityService<User>>
    {
        IUserService _theService;
     
        public UserController(IUserService theService)
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
        public ActionResult ListForCombo()
        {
            var data = _theService.GetAll().ToList();

            var results = data.Select(s => new UserViewModel
            {
                Id = s.Id,
                Account = s.Account,
                Name = s.Name,
                LastName = s.LastName,
                Email = s.Email
            }).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }


        [View]
        public JsonResult List([DataSourceRequest] DataSourceRequest users)
        {
            var userList = _theService.GetAllWithIncludings("UserTitle");

            var results = userList.ToDataSourceResult(users, s => new UserViewModel()
            {
                Id = s.Id,
                Account = s.Account,
                Name = s.Name,
                LastName = s.LastName,
                Email = s.Email,
                UserTitleText = s.UserTitle.Description
            });
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        
        [Save]
        [HttpGet]
        public ActionResult Edit(int id)
        {
           return base.EditGet(id, "UserTitle");
        }

        [HttpPost]
        [Save]
        public ActionResult Edit(int id, UserViewModel formUser )
        {
            return base.EditPost(formUser);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return base.DeletePost(id);
        }



    }
}
