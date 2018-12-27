using Kendo.Mvc.UI;
using SampleArch.Model;
using SampleArch.Model.ViewModels;
using SampleArch.Service.Admin;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Positive.Controllers
{
   // [ModuleAuthorize]
    public class HomeController : Controller
    {

        readonly IUserManagementService userManagementService;
        public HomeController(IUserManagementService userManagementServiceFor)
        {
            userManagementService = userManagementServiceFor;
        }
        public ActionResult Index()
        {

            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult MainPage()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            var userProfile =  ToolBox.GetUserProfile();

            if (userProfile == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Account");
            }

            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult DashBoard()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }


        public JsonResult MenuData()
        {
            var data = UserManagementService.GetAllowedMenusViewModelInRolesByUser();

            var result = Json(data, JsonRequestBehavior.AllowGet);

            return result;
        }
    }
}
