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
using SampleArch.Service.Core;
using SampleArch.Service.CRM;
using SampleArch.Service;
using SampleArch.Modules;

namespace Positive.Controllers
{
   
    [ModuleAuthorize("Admin")]
    public class ProjectController : PositiveController<Project, ProjectViewModel, IEntityService<Project>>
    {
        IProjectService _projectService;

        public ProjectController(IProjectService projectService)
            : base("admin", projectService)
        {
             _projectService = projectService;
        }

        // GET: Category
        [View]
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult List([DataSourceRequest] DataSourceRequest filter)
        {
            var dataList = _projectService.GetAll().ToList();

            var results = dataList.ToDataSourceResult(filter, s => new ProjectViewModel()
            {
                Id = s.Id,
                ProjectName = s.ProjectName,
                Description  = s.Description

            });
            return Json(results, JsonRequestBehavior.AllowGet);
        }



        [AllowAnonymous]
        [OutputCache(Duration = 100, VaryByParam = "none")]
        public ActionResult ListForCombo()
        {
            var data = _projectService.GetAll().ToList();

            var results = data.Select(s => new ProjectViewModel
            {
                Id = s.Id,
                ProjectName = s.ProjectName
            }).ToList();

            return Json(results, JsonRequestBehavior.AllowGet);
        }
       

        // GET: Category/Edit/5
        [Save]
        public ActionResult Edit(int id)
        {
            return base.EditGet(id, null);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [Save]
        public ActionResult Edit(ProjectViewModel viewModel)
        {
            return base.EditPost(viewModel);
        }

     
        // POST: Category/Delete/5
        [HttpPost]
        [Delete]
        public ActionResult Delete(int id)
        {
            return base.DeletePost(id);
        }
    }
}
