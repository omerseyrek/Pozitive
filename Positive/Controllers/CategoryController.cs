using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using SampleArch.Model;
using SampleArch.Model.ViewModels;
using SampleArch.Service.Core;
using System.Linq;
using System.Web.Mvc;
using SampleArch.Service.Stock;
using SampleArch.Modules;
using SampleArch.Service;
using SampleArch.Model.Core;

namespace Positive.Controllers
{
    [ModuleAuthorize("stock")]
    public class CategoryController : PositiveController<Category, CategoryViewModel, IEntityService<Category>>
    {
        ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
            : base("stock", categoryService)
        {
            _categoryService = categoryService;
        }


        [View]
        public ActionResult Index()
        {
            return View();
        }



        [AllowAnonymous]
        public JsonResult ListForSearch([DataSourceRequest] DataSourceRequest request)
        {
            var data = _categoryService.GetAll().ToList();

            var results = data.Select(s => new CategoryViewModelForSearch
            {
                Id = s.Id,
                Key = s.Key,
                CompleteKey = s.CompleteKey,
                Name = s.Name
            }).ToList();

            var res = results.ToDataSourceResult(request);

            var tosend = Json(results, JsonRequestBehavior.AllowGet);

            return tosend;
        }
       



        [View]
        public JsonResult All([DataSourceRequest] DataSourceRequest categories)
        {
            var categoryList = _categoryService.GetAll().ToList();

            var result = categoryList.ToTreeDataSourceResult(categories,
                 e => e.Id,
                 e => e.ParentId,
                 e => new CategoryViewModel {
                     Id = e.Id,
                     ParentId = e.ParentId,
                     Key = e.Key,
                     CompleteKey = e.CompleteKey,
                     Description = e.Description,
                     Name = e.Name
                 }
             );
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [View]
        public ActionResult Details(int id)
        {
            var category = _categoryService.Get(id);
            return View(category);
        }




        [Save]
        public ActionResult Edit(int id)
        {
            CategoryViewModel model = new CategoryViewModel();
          
            if (id > 0)
            {
                var category = new Category();
                category = _categoryService.Get(id);

                model.CompleteKey = category.CompleteKey;
                model.Description = category.Description;
                model.Id = category.Id;
                model.Key = category.Key;
                model.Name = category.Name;
                model.ParentId = category.ParentId;

                if (category.ParentId.HasValue)
                {
                    var parent = _categoryService.Get(category.ParentId.Value);

                    CategoryViewModel parentModel = new CategoryViewModel();
                    parentModel.CompleteKey = parent.CompleteKey;
                    parentModel.Description = parent.Description;
                    parentModel.Id = parent.Id;
                    parentModel.Key = parent.Key;
                    parentModel.Name = parent.Name;
                    parentModel.ParentId = parent.ParentId;

                    model.ParentCategory = parentModel;
                }
            }

            return View(model);
        }

        [HttpPost]
        [Save]
        public ActionResult Edit(int id, CategoryViewModel category)
        {
            string[] codeParts = category.CompleteKey.Split('.');

            codeParts[codeParts.Length - 1] = category.Key;

            category.CompleteKey = string.Join(".", codeParts);
            
            return DoSave(id, category);
        }




        [Save]
        public ActionResult AddRoot()
        {
            CategoryViewModel model = new CategoryViewModel();

            model.CompleteKey = "";
            model.Key = "";
            model.Description = "";
            model.Name = "";

            return View("Edit", model);
        }

        [HttpPost]
        [Save]
        public ActionResult AddRoot(int id, CategoryViewModel category)
        {
            category.CompleteKey = category.Key;
            return DoSave(id, category);
        }





        [Save]
        public ActionResult AddChild(int Id = 0)
        {
            CategoryViewModel model = new CategoryViewModel();

            Category parent = _categoryService.Get(Id);

            CategoryViewModel parentModel = new CategoryViewModel();
            parentModel.CompleteKey = parent.CompleteKey;
            parentModel.Description = parent.Description;
            parentModel.Id = parent.Id;
            parentModel.Key = parent.Key;
            parentModel.Name = parent.Name;
            parentModel.ParentId = parent.ParentId;



            model.CompleteKey = "";
            model.ParentCategory = parentModel;
            model.ParentId = parent.Id;

            model.ParentCategory = parentModel;

            return View("Edit", model);
        }

        [HttpPost]
        [Save]
        public ActionResult AddChild(CategoryViewModel category)
        {
            CategoryViewModel model = new CategoryViewModel();

            Category parent = _categoryService.Get(category.ParentId);

            category.Id = 0;
            category.CompleteKey = parent.CompleteKey + "." + category.Key;
            category.Description = category.Description;
            category.Key = category.Key;
            category.Name = category.Name;
            category.ParentId = category.ParentId;

            return DoSave(0, category);
        }



        private ActionResult DoSave(int id, CategoryViewModel category)
        {
            return base.EditPost(category);
        }

        [HttpPost]
        [Delete]
        public ActionResult Delete(int id, Category c)
        {
            return base.DeletePost(id);
        }
    }
}