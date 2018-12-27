using Positive;
using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.ViewModels;
using SampleArch.Service;
using SampleArch.Service.Admin;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SampleArch.Modules
{
    public abstract class PositiveController<TEntitiy, TViewModel, TService> : Controller
        where TEntitiy : Entity<int>, new()
        where TViewModel : BaseViewModel, new()
        where TService : IEntityService<TEntitiy>
    {
        protected ModulesInRolesViewModel pageAuthorisation;
        public IUserManagementService _umService;

        public TService TheService;

        public PositiveController(string moduleCode, TService theService)
        {
            TheService = theService;
            _umService = (IUserManagementService)DependencyResolver.Current.GetService(typeof(IUserManagementService));

            pageAuthorisation = _umService.GetAllowedRolesOfUserByModuleCode(moduleCode);
            ViewBag.PageAthority = pageAuthorisation;

            ViewBag.UICulture = ToolBox.GetUserProfileCulture();
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            Thread.CurrentThread.CurrentCulture = ToolBox.GetUserProfileCulture();
            Thread.CurrentThread.CurrentUICulture = ToolBox.GetUserProfileCulture();

            base.Initialize(requestContext);
        }

        public TEntitiy CreateEntity()
        {
            return Activator.CreateInstance<TEntitiy>();
        }

        public TViewModel CreateModel()
        {
            return Activator.CreateInstance<TViewModel>();
        }

        public ActionResult EditGet(int id, string includings)
        {
            TEntitiy dataEntity = new TEntitiy();
            if (id > 0)
            {
                if (string.IsNullOrEmpty(includings))
                {
                    dataEntity = TheService.Get(id);
                }
                else
                {
                    dataEntity = TheService.Get(id);
                }
            }

            TViewModel model = PocoHelper.BasicPocoMapper<TEntitiy, TViewModel>(dataEntity);

            return View(model);
        }

        public ActionResult EditPost(TViewModel viewModel)
        {
            PositiveResults pr = new PositiveResults();
            List<ValidationResult> validations = new List<ValidationResult>();
            try
            {
                if (ModelState.IsValid)
                {
                    var userProfile = ToolBox.GetUserProfile();

                    TEntitiy data = new TEntitiy();

                    if (viewModel.Id > 0)
                    {
                        data = TheService.Get(viewModel.Id);
                    }

                    //to do also inner object such as stock and stock image datas save at once

                    PocoHelper.UpdatePocoMapper<TViewModel, TEntitiy>(viewModel, data);

                    PocoHelper.SetTractionFieldsOfEntitiy(data, Int32.Parse(userProfile.UserId), DateTime.Now);

                    validations = TheService.GetBussinesValidations(data).ToList();
                    if (validations.Count > 0)
                    {
                        // do nothing..
                    }
                    else
                    {

                        if (viewModel.Id > 0)
                        {
                            TheService.Update(data);
                        }
                        else
                        {
                            TheService.Create(data);
                        }
                        return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    validations.Add(new ValidationResult()
                    {
                        MemberName = "",
                        Message = "model state is invalid",
                        MessType = MessageType.Error
                    });
                }
            }
            catch
            {
                ValidationResult vrException = new ValidationResult()
                {
                    MemberName = "",
                    Message = "bir hata oluşu ve loglandı.",
                    MessType = MessageType.Error
                };

                validations.Add(vrException);
            }

            if (validations.Count > 0)
            {
                pr.Success = false;

                pr.AddResultsRange(validations);
            }
            else
            {
                pr.Success = true;
            }

            return Json(pr, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeletePost(int id)
        {
            PositiveResults pr = new PositiveResults();
            List<ValidationResult> validations = new List<ValidationResult>();

            try
            {
                var data = TheService.Get(id);

                TheService.Delete(data);

                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                string message = ex.Message;
                IExceptionManager exMan = FactoryResolver.GetExceptionManager();

                if (exMan.IsReferenceConstraint(ex) == true)
                {
                    message = Positive.Model.Languages.Common.DeleteWarningForerelationalData;
                }

                validations.Add(new ValidationResult()
                {
                    MemberName = "",
                    Message = message,
                    MessType = MessageType.Error
                });

                if (validations.Count > 0)
                {
                    pr.Success = false;

                    pr.AddResultsRange(validations);
                }
                else
                {
                    pr.Success = true;
                }

                return Json(pr, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
