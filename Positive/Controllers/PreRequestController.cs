using Kendo.Mvc.UI;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using SampleArch.Modules;
using SampleArch.Service;
using SampleArch.Service.Core;
using SampleArch.Service.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace Positive.Controllers
{
 

    [ModuleAuthorize("pre")]
    public class PreRequestController : PositiveController<PreRequest, PreRequestViewModel, IEntityService<PreRequest>>
    {
        IPreRequestService _theService;
        public PreRequestController(IPreRequestService theService)
            : base("pre", theService)
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
            var dataList = _theService.GetAllWithIncludings("Project");

            var results = dataList.ToDataSourceResult(filter, s => new PreRequestViewModel()
            {
                Id = s.Id,
                DeadLineDate = s.DeadLineDate,
                Description = s.Description,
                ProjectName = s.Project.ProjectName,
                RequestDate = s.RequestDate

            });
            return Json(results, JsonRequestBehavior.AllowGet);
            
        }

   
        [Save]
        public ActionResult Edit(int id)
        {
            return base.EditGet(id, "Project");
        }

        
        [HttpPost]
        [Save]
        public ActionResult Edit(PreRequestViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userProfile = ToolBox.GetUserProfile();

                    var sublist = Request.Form["subGridItems"];

                    var destroyed = Request.Form["subGridDestroyedItems"];

                    viewModel.RequestLines = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RequestLineViewModel>>(sublist);


                    foreach (RequestLineViewModel line in viewModel.RequestLines)
                    {
                        line.StockUnitId = 1;
                    }

                    viewModel.DestroyedIDs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(destroyed);

                    if (viewModel.Id > 0)
                    {
                        _theService.UpdateWithLines(viewModel);
                    }
                    else
                    {
                        _theService.CreateWithLines(viewModel);
                    }
                    return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (DbEntityValidationException eve)
            {
                string resulttext = "";
                 foreach (var ve in eve.EntityValidationErrors)
                    {
                       resulttext += eve.EntityValidationErrors.ToList().Count().ToString();
                    }

                 return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

     
     
        [HttpPost]
        [Delete]
        public ActionResult Delete(int id)
        {
            return base.DeletePost(id);
        }

    }
}
