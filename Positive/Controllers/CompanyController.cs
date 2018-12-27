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

namespace Positive.Controllers
{
    [ModuleAuthorize("CRM")]
    public class CompanyController : PositiveController<Company, CompanyViewModel, IEntityService<Company>>
    {
        ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
            : base("CRM", companyService)
        {
            _companyService = companyService;
        }
      

        [View]
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult List([DataSourceRequest] DataSourceRequest filter)
        {
            var dataList = _companyService.GetAll().ToList();

            var results = dataList.ToDataSourceResult(filter, s => new CompanyViewModel()
            {
                Id = s.Id,
                FirmName = s.FirmName,
                MersisNo = s.MersisNo,
                Note = s.Note,
                TaxNumber = s.TaxNumber,
                TaxOffice = s.TaxOffice
            });
            return Json(results, JsonRequestBehavior.AllowGet);
        }



        [AllowAnonymous]
        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult ListForCombo()
        {
            var data = _companyService.GetAll().ToList();

            var results = data.Select(s => new CompanyViewModel
            {
                Id = s.Id,
                FirmName = s.FirmName
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
        public ActionResult Edit(CompanyViewModel viewModel)
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