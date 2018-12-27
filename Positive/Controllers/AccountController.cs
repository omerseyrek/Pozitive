using SampleArch.Model.ViewModels;
using SampleArch.Model.ViewModels.Admin;
using SampleArch.Modules;
using SampleArch.Service.Admin;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Positive.Controllers
{
    [ModuleAuthorize]
    public class AccountController : Controller
    {
        readonly IUserManagementService _userManagementService;
        public AccountController(IUserManagementService userManagementServiceFor)
        {
          
            _userManagementService = userManagementServiceFor;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            string langs = requestContext.HttpContext.Request.UserLanguages[0];
            if (langs.Contains("en"))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
            }
            else if( langs.Contains("en"))
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("tr-TR");
            }

            base.Initialize(requestContext);
        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if(string.IsNullOrEmpty(returnUrl))
            {
                RedirectToAction("Home", "MainPage");
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogonViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_userManagementService.ValidateUser(model.UserName, model.Password))
                {
                    var user = _userManagementService.GetUserByName(model.UserName);

                    if (user == null)
                    {
                        ModelState.AddModelError("UserName", Positive.Model.Languages.Admin.ValUserIsNotAuthorized);
                        return View(model);
                    }

                    Session["UserName"] = user.Account;
                    Session["UserId"] = user.Id;

                    if (user.IsActive == false || user.IsLocked == 1)
                    {
                        ModelState.AddModelError("", Positive.Model.Languages.Admin.ValUserLocked);
                        return View(model);
                    }

                    UserProfileViewModel profile = new UserProfileViewModel();

                    profile.Account = user.Account;
                    profile.Id = user.Id.ToString();
                    profile.Name = model.UserName;
                    profile.UserId = user.Id.ToString();
                    profile.LanguageCode = model.LanguageCode;

                    Session.Add("_userProfile", profile);

                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("MainPage", "Home");
                }
                ModelState.AddModelError("", Positive.Model.Languages.Admin.ValInvalidUserOrPWD);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        [HttpPost]
        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


        public ActionResult TreeMenu()
        {
            List<MenusViewModel> treeMenu = UserManagementService.GetAllowedMenusViewModelInRolesByUser();

            return Json(treeMenu, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AuthorizeError()
        {
            return View();
        }

        
    }
}