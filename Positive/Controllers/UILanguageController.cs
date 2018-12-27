using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using SampleArch.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Positive.Controllers
{

    public class UILanguageController : Controller
    {
        IUILanguageService _Service;
        public UILanguageController(IUILanguageService langService)
        {
            _Service = langService;
        }

        [AllowAnonymous]
        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult List()
        {
            var data = _Service.GetAll().ToList();

            var results = data.Select(s => new UILanguageViewModel
            {
                Id = s.Id,
                LangCode = s.LangCode,
                Description = s.Description
            }).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);                          
        }
    }
}