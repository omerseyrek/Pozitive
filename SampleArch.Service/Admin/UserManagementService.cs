using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.ViewModels;
using SampleArch.Repository.Admin;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SampleArch.Service.Admin
{
    public class UserManagementService : IUserManagementService
    {
        /// <summary>
        /// 
        /// </summary>
        private static ICacheProvider _cache;
        public static ICacheProvider Cache
        {
            get { return _cache ?? (_cache = new DefaultCacheProvider()); }
            set { _cache = value; }
        }

        public static IUserRepository userRepository;
        public static IRoleRepository roleRepository;
        public static IModuleRepository moduleRepository;
        public static IModulesInRoleRepository modulesInRoleRepository;
        public static IMenuRepository menuRepository;

        public static DbContext dbcontext;

        public UserManagementService()
        {
            dbcontext = new SampleArchContext();
            userRepository = FactoryResolver.GetUserRepository(dbcontext);
            roleRepository = FactoryResolver.GetRoleRepository(dbcontext);
            moduleRepository = FactoryResolver.GetModuleRepository(dbcontext);
            modulesInRoleRepository = FactoryResolver.GetModulesInRolesRepository(dbcontext);
        }

        public static int GetCurrentUserId()
        {
            var userName = HttpContext.Current.User.Identity.Name;

            var currentUser = userRepository.GetBy(p => p.Account == userName);

            if (currentUser != null)
            {
                return currentUser.Id;
            }
            return 0;
        }

        public static bool IsAdminUser()
        {
            var userName = HttpContext.Current.User.Identity.Name;

            var currentUser = userRepository.GetBy(p => p.Account == userName && p.UsersInRoles.Any(r => r.Roles.Code == ApplicationConstants.SuperUser));

            if (currentUser != null)
            {
                return true;
            }
            return false;
        }

        public static List<ModulesInRolesViewModel> GetAllModulesPermissions()
        {
            var models = new List<ModulesInRolesViewModel>();
            List<ModulesViewModel> moduleViewModel = GetModules();
            if (moduleViewModel != null && moduleViewModel.Count > 0)
            {
                models.AddRange(moduleViewModel.Select(model => new ModulesInRolesViewModel
                    {
                        Id = model.Id,
                        View = true,
                        Add = true,
                        Update = true,
                        Delete = true
                    }));
            }
            return models;
        }

        public static List<ModulesInRolesViewModel> GetAllowedModulesInRolesByUser()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string cacheName = string.Format("{0}{1}", ApplicationConstants.ModuleCachePrefix, userName);


            var data = Cache.Get(cacheName);
            if (data == null)
            {
                var userID = GetCurrentUserId();

                var roleIds = roleRepository.GetRolesOfUser(userID).Select(p => p.Id);

                var allowedModules = modulesInRoleRepository.FindBy(p => roleIds.Contains(p.RoleId) && (p.Add || p.Delete || p.Update || p.View))
                                      .Select(p => new ModulesInRolesViewModel()
                                      {
                                          Id = p.Id,
                                          ModuleId = p.ModuleId,
                                          RoleId = p.RoleId,
                                          Add = p.Add,
                                          Delete = p.Delete,
                                          Update = p.Update,
                                          View = p.View
                                      }).ToList();

                Cache.Set(cacheName, allowedModules, 30);

                return allowedModules;
            }

            return data as List<ModulesInRolesViewModel>;
        }

        public ModulesInRolesViewModel GetAllowedRolesOfUserByModuleCode(string moduleCode)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string cacheName = string.Format("{0}{1}", ApplicationConstants.ModuleCachePrefix, userName);

            ModulesInRolesViewModel mergedRolesInModule = new ModulesInRolesViewModel()
            {
                ModuleCode = moduleCode
            };

            var data = Cache.Get(cacheName);
            if (data == null)
            {
                GetAllowedModulesInRolesByUser();
            }

            if (data != null)
            {
                var userID = GetCurrentUserId();

                var roleIds = roleRepository.GetRolesOfUser(userID).Select(p => p.Id);

                var allowedModules = modulesInRoleRepository.FindBy(p =>
                                                    p.Module.Code == moduleCode &&
                                                    roleIds.Contains(p.RoleId) && (p.Add || p.Delete || p.Update || p.View))
                                      .Select(p => new ModulesInRolesViewModel()
                                      {
                                          Id = p.Id,
                                          ModuleId = p.ModuleId,
                                          RoleId = p.RoleId,
                                          Add = p.Add,
                                          Delete = p.Delete,
                                          Update = p.Update,
                                          View = p.View
                                      }).ToList();



                foreach (ModulesInRolesViewModel m in allowedModules)
                {
                    if (m.Add)
                    {
                        mergedRolesInModule.Add = true;
                    }
                    if (m.Delete)
                    {
                        mergedRolesInModule.Delete = true;
                    }
                    if (m.Update)
                    {
                        mergedRolesInModule.Update = true;
                    }
                    if (m.View)
                    {
                        mergedRolesInModule.View = true;
                    }
                }
            }

            return mergedRolesInModule;
        }

        public static List<MenusViewModel> GetAllowedMenusViewModelInRolesByUser()
        {
            var userName = HttpContext.Current.User.Identity.Name;
            var cacheName = string.Format("{0}{1}", ApplicationConstants.MenuCachePrefix, userName);

            var data = Cache.Get(cacheName);

            if (data == null)
            {

                List<ModulesInRolesViewModel> modules = GetAllowedModulesInRolesByUser();
                var moduleIds = modules.Select(p => p.ModuleId).ToList();

                SampleArchContext ctx = dbcontext as SampleArchContext;

                var parents = ctx.Menus.Include(x => x.SubMenu).Where(p => p.ParentId.HasValue == false).ToList();

                foreach (Menu mvm in parents)
                {
                    clearDeadNodes(mvm, moduleIds);
                }

                List<MenusViewModel> menus = new List<MenusViewModel>();
                foreach (Menu mvm in parents)
                {
                    MenusViewModel menuModel = new MenusViewModel();
                    ConvertToMenuViewModel(mvm, menuModel);
                    menus.Add(menuModel);
                }

                Cache.Set(cacheName, menus, 30);

                return menus;
            }

            return data as List<MenusViewModel>;
        }

        public static List<Menu> GetAllowedMenusInRolesByUser()
        {
            var userName = HttpContext.Current.User.Identity.Name;
            var cacheName = string.Format("{0}Node{1}", ApplicationConstants.MenuCachePrefix, userName);

            var data = Cache.Get(cacheName);

            if (data == null)
            {

                List<ModulesInRolesViewModel> modules = GetAllowedModulesInRolesByUser();
                var moduleIds = modules.Select(p => p.ModuleId).ToList();

                SampleArchContext ctx = dbcontext as SampleArchContext;

                var parents = ctx.Menus.Include(x => x.SubMenu).Where(p => p.ParentId.HasValue == false).ToList();

                foreach (Menu mvm in parents)
                {
                    clearDeadNodes(mvm, moduleIds);
                }

                Cache.Set(cacheName, parents, 30);

                return parents;
            }

            return data as List<Menu>;
        }

        public static void clearDeadNodes(Menu item, List<int> moduleIds)
        {
            if ((item.ModuleId ?? -1) > 0)
            {
                if (!moduleIds.Contains(item.ModuleId ?? -1))
                {
                    item.Parent.SubMenu.Remove(item);
                }
                return;
            }
            else if (item.Parent != null && (item.SubMenu == null || item.SubMenu.Count == 0))
            {
                item.Parent.SubMenu.Remove(item);
            }
            else
            {
                for (int i = item.SubMenu.Count - 1; i >= 0; i--)
                {
                    var mvm = item.SubMenu[i];
                    clearDeadNodes(mvm, moduleIds);
                }
            }
        }

        public static void ConvertToMenuViewModel(Menu item, MenusViewModel model)
        {
            model.Id = item.Id;
            model.ParentId = item.ParentId;
            model.ModuleId = item.ModuleId;
            model.Description = item.Description;
            model.DescriptionLowered = item.Code;
            model.IsActive = item.IsActive;
            model.Url = item.URL;

            if (item.SubMenu != null && item.SubMenu.Count > 0)
            {
                model.hasChildren = true;
                model.SubMenus = new List<MenusViewModel>();
                foreach (Menu submenu in item.SubMenu)
                {
                    MenusViewModel subMenuModel = new MenusViewModel();
                    ConvertToMenuViewModel(submenu, subMenuModel);
                    model.SubMenus.Add(subMenuModel);
                }
            }
            else
            {
                model.hasChildren = false;
            }
        }

        public static List<ModulesViewModel> GetModules()
        {
            var data = Cache.Get("modules");
            if (data == null)
            {
                var modules = moduleRepository.GetAll()
                             .Select(p => new ModulesViewModel()
                                {
                                    Description = p.Description,
                                    Id = p.Id,
                                    IsActive = p.IsActive
                                }
                            ).ToList();
                Cache.Set("module", modules, 30);
                return modules;
            }
            return (List<ModulesViewModel>)Cache.Get("modules");
        }

        public static List<int> GetAllowedModulesByUser()
        {
            var moduleInRolesViewModel = GetAllowedModulesInRolesByUser();
            if (moduleInRolesViewModel == null) return null;
            return moduleInRolesViewModel.Select(p => p.ModuleId).ToList();
        }

        public static ModulesInRolesViewModel GetModulePermissionsByModule(string moduleCode)
        {
            if (string.IsNullOrEmpty(moduleCode)) return null;
            moduleCode = moduleCode.ToLowerInvariant();
            var moduleId = moduleRepository.GetBy(p => p.Code == moduleCode).Id;
            return moduleId == 0 ? null : GetModulePermissionsByModule(moduleId);
        }

        public static ModulesInRolesViewModel GetModulePermissionsByModule(int moduleId)
        {
            if (moduleId <= 0) return null;
            var moduleInRolesViewModel = GetAllowedModulesInRolesByUser();
            return moduleInRolesViewModel == null ? null : moduleInRolesViewModel.FirstOrDefault(p => p.ModuleId == moduleId);
        }

        public static bool HasPermission(string moduleCode, ProcessTypes process)
        {
            if (string.IsNullOrEmpty(moduleCode)) return false;

            if (IsAdminUser()) return true;


            moduleCode = moduleCode.ToLowerInvariant();

            var moduleId = moduleRepository.GetBy(p => p.Code == moduleCode).Id;
            if (moduleId == 0) return false;
            return HasPermission(moduleId, process);
        }

        public static bool HasPermission(int moduleId, ProcessTypes process)
        {
            if (moduleId <= 0) return false;
            if (IsAdminUser()) return true;

            var moduleInRolesViewModel = GetAllowedModulesInRolesByUser();
            if (moduleInRolesViewModel == null) return false;
            return moduleInRolesViewModel.FirstOrDefault(p => (p.ModuleId) == moduleId &&
                                                              ((process == ProcessTypes.View && p.View) ||
                                                               (process == ProcessTypes.Add && p.Add) ||
                                                               (process == ProcessTypes.Update && p.Update) ||
                                                               (process == ProcessTypes.Delete && p.Delete))) != null;
        }

        public bool ValidateUser(string username, string password)
        {
            var loweredName = username.ToLowerInvariant();
            if (userRepository.GetBy(p => p.Account == loweredName && p.Password == password) != null)
            {
                return true;
            }
            return false;
        }

        public User GetUserByName(string username)
        {
            var loweredName = username.ToLowerInvariant();
            var user = userRepository.GetBy(p => p.Account == loweredName);
            return user;
        }



    }
}
