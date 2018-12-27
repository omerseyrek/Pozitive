using Newtonsoft.Json;
using SampleArch.Model.Core;
using SampleArch.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SampleArch.Service.Core
{
    class Authorize
    {
    }

    public class ViewAttribute : ActionFilterAttribute
    {
        readonly object _emptyValue;
        readonly string _key;
        readonly Type _type;
        public ViewAttribute()
            : this(typeof(int))
        {
        }


        public ViewAttribute(Type type)
            : this(type, "id")
        {

        }

        public ViewAttribute(Type type, string key)
        {
            _emptyValue = Activator.CreateInstance(type);
            _key = key;
            _type = type;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] modules = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(ModuleAuthorizeAttribute), false);

            if (modules.Length > 0)
            {
                object idValue = null;
                if (filterContext.ActionParameters.ContainsKey(_key))
                    filterContext.ActionParameters.TryGetValue(_key, out idValue);

                if (_type == typeof(Guid))
                {
                    if (idValue != null)
                    {
                        Guid val;
                        Guid.TryParse(idValue.ToString(), out val);
                        idValue = val;
                    }
                }
                else
                {
                    if (idValue != null)
                    {
                        try { idValue = Convert.ChangeType(idValue, _type); }
                        catch { idValue = _emptyValue; }
                    }
                }

                foreach (var module in modules)
                {
                    var mod = module as ModuleAuthorizeAttribute;
                    if (idValue != null && _emptyValue != null && idValue.ToString().Equals(_emptyValue.ToString()))
                    {
                        if (mod != null)
                        {
                            var permissions = UserManagementService.GetModulePermissionsByModule(mod.Module);
                            if (permissions == null || (permissions.View != true && permissions.Add != true && permissions.Update != true && permissions.Delete != true))
                            {
                                filterContext.Result = new HttpUnauthorizedResult();
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (mod != null && !UserManagementService.HasPermission(mod.Module, ProcessTypes.View))
                        {
                            filterContext.Result = new HttpUnauthorizedResult();
                            break;
                        }
                    }
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            base.OnActionExecuting(filterContext);
        }

    }

    public class SaveAttribute : ActionFilterAttribute
    {
        readonly object _emptyValue;
        readonly string _key;
        readonly Type _type;
        public SaveAttribute()
            : this(typeof(int))
        {

        }

        public SaveAttribute(Type type)
            : this(type, "id")
        {

        }

        public SaveAttribute(Type type, string key)
        {
            _emptyValue = Activator.CreateInstance(type);
            _key = key;
            _type = type;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] modules = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(ModuleAuthorizeAttribute), false);
            if (modules.Length > 0)
            {

                object idValue = null;
                if (filterContext.ActionParameters.ContainsKey(_key))
                    filterContext.ActionParameters.TryGetValue(_key, out idValue);


                if (_type == typeof(Guid))
                {
                    if (idValue != null)
                    {
                        Guid val;
                        Guid.TryParse(idValue.ToString(), out val);
                        idValue = val;
                    }
                }
                else
                {
                    if (idValue != null)
                    {
                        try { idValue = Convert.ChangeType(idValue, _type); }
                        catch { idValue = _emptyValue; }
                    }
                }

                foreach (var module in modules)
                {
                    var mod = module as ModuleAuthorizeAttribute;
                    if (mod != null && (!UserManagementService.HasPermission(mod.Module, (idValue == CoreFunctions.GetDefaultValue(_type)) ? ProcessTypes.Add : ProcessTypes.Update)))
                    {
                        filterContext.Result = new HttpUnauthorizedResult();
                        break;
                    }
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            base.OnActionExecuting(filterContext);
        }


    }

    public class EditAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] modules = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(ModuleAuthorizeAttribute), false);

            if (modules.Length > 0)
            {
                var isUpdated = bool.Parse(filterContext.HttpContext.Request.Form["IsFormUpdated"]);
                var isDeleted = bool.Parse(filterContext.HttpContext.Request.Form["IsFormDeleted"]);
                var isInserted = bool.Parse(filterContext.HttpContext.Request.Form["IsFormInserted"]);

                foreach (var module in modules)
                {
                    var mod = module as ModuleAuthorizeAttribute;
                    if (mod == null || ((isUpdated != true || UserManagementService.HasPermission(mod.Module, ProcessTypes.Update)) && (isInserted != true || UserManagementService.HasPermission(mod.Module, ProcessTypes.Add)) && (isDeleted != true || UserManagementService.HasPermission(mod.Module, ProcessTypes.Delete))) == false)
                    {
                        var obj = new { message = "not authorized to do this ..." };


                        var result = new PositiveResults
                        {
                            Success = false
                        };

                        var vr = new ValidationResult()
                        {
                            MemberName = "Authorization",
                            Message = "do not have permission to do this action."
                        };
                        result.Messages = new List<ValidationResult>();
                        result.Messages.Add(vr);

                        var authcontent = JsonConvert.SerializeObject(result);


                        var cr = new ContentResult
                        {
                            Content = authcontent,
                            ContentType = "application/json"
                        };

                        filterContext.Result = cr;//new HttpUnauthorizedResult();
                        break;
                    }
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class UpdateAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] modules = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(ModuleAuthorizeAttribute), false);
            if (modules.Length > 0)
            {

                foreach (var module in modules)
                {
                    var mod = module as ModuleAuthorizeAttribute;
                    if (mod == null || (UserManagementService.HasPermission(mod.Module, ProcessTypes.Update)) == false)
                    {
                        filterContext.Result = new HttpUnauthorizedResult();
                        break;
                    }
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class DeleteAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] modules = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(ModuleAuthorizeAttribute), false);
            if (modules.Length > 0)
            {
                foreach (var module in modules)
                {
                    var mod = module as ModuleAuthorizeAttribute;
                    if (mod != null && !UserManagementService.HasPermission(mod.Module, ProcessTypes.Delete))
                    {
                        filterContext.Result = new HttpUnauthorizedResult();
                        break;
                    }
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            base.OnActionExecuting(filterContext);
        }


    }
}
