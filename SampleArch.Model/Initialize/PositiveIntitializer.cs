using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Initialize
{
    public class PositiveIntitializer : CreateDatabaseIfNotExists<SampleArchContext>
    {
        public override void InitializeDatabase(SampleArchContext context)
        {  

            Seed(context);
        }



        protected override void Seed(SampleArchContext context)
        {
            #region Add Users
            User omer = new User()
            {
                Name = "ömer",
                LastName = "seyrek",
                Account = "oseyrek",
                Password = "123456",
                Email = "omerseyrek@hotmail.com",
                IsActive = true
            };

            if (context.Users.FirstOrDefault(p => p.Account == "oseyrek") == null)
            {
                context.Users.Add(omer);
                context.SaveChanges();
            }

            omer.Id = context.Users.FirstOrDefault(p => p.Account == "oseyrek").Id;

            User erman = new User()
            {
                Name = "erman",
                LastName = "demirer",
                Account = "edemir",
                Password = "123456",
                Email = "edemir@hotmail.com",
                IsActive = true
            };

            if (context.Users.FirstOrDefault(p => p.Account == "edemirer") == null)
            {
                context.Users.Add(erman);
                context.SaveChanges();
            }
            erman.Id = context.Users.FirstOrDefault(p => p.Account == "edemirer").Id;

            User tolga = new User()
            {
                Name = "tolga",
                LastName = "test",
                Account = "tolgat",
                Password = "123456",
                Email = "tolgat@hotmail.com",
                IsActive = true
            };

            if (context.Users.FirstOrDefault(p => p.Account == "tolgat") == null)
            {
                context.Users.Add(tolga);
                context.SaveChanges();
            }

            tolga.Id = context.Users.FirstOrDefault(p => p.Account == "tolgat").Id;

            #endregion


            #region Add Roles
            Role Admin = new Role()
            {
                Code = "admin",
            };

            if (context.Roles.FirstOrDefault(p => p.Code == "admin") == null)
            {
                context.Roles.Add(Admin);
                context.SaveChanges();
            }

            Admin.Id = context.Roles.FirstOrDefault(p => p.Code == "admin").Id;
            

            Role StokRole = new Role()
            {
                Code = "stock",
            };

            if (context.Roles.FirstOrDefault(p => p.Code == "stock") == null)
            {
                context.Roles.Add(StokRole);
                context.SaveChanges();
            }
            StokRole.Id = context.Roles.FirstOrDefault(p => p.Code == "stock").Id;
            #endregion


            #region AddUserInRoles
            UsersInRole r1 = new UsersInRole()
            {
                UserID = omer.Id,
                RoleId = Admin.Id
            };
            if (context.UsersInRoles.FirstOrDefault(p => p.UserID == omer.Id && p.RoleId == Admin.Id) == null)
            {
                context.UsersInRoles.Add(r1);
            }

            UsersInRole r2 = new UsersInRole()
            {
                UserID = erman.Id,
                RoleId = Admin.Id
            };
            if (context.UsersInRoles.FirstOrDefault(p => p.UserID == erman.Id && p.RoleId == Admin.Id) == null)
            {
                context.UsersInRoles.Add(r2);
            }

            UsersInRole r3 = new UsersInRole()
            {
                UserID = tolga.Id,
                RoleId = StokRole.Id
            };
            if (context.UsersInRoles.FirstOrDefault(p => p.UserID == tolga.Id && p.RoleId == StokRole.Id) == null)
            {
                context.UsersInRoles.Add(r3);
            }
            context.SaveChanges();
            #endregion


            #region Modules
            Module adminModule = new Module()
            {
                Description = "Admin",
                Code = "admin",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Now,
            };

            if (context.Modules.FirstOrDefault(p => p.Code == "admin") == null)
            {
                context.Modules.Add(adminModule);
                context.SaveChanges();
            }
            adminModule.Id = context.Modules.FirstOrDefault(p => p.Code == "admin").Id;

            Module stockModule = new Module()
            {
                Description = "Stok",
                Code = "stock",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Now,
            };

            if (context.Modules.FirstOrDefault(p => p.Code == "stock") == null)
            {
                context.Modules.Add(stockModule);
                context.SaveChanges();
            }
            stockModule.Id = context.Modules.FirstOrDefault(p => p.Code == "stock").Id;


            #endregion


            #region ModulesInRoles
            ModulesInRole mr1 = new ModulesInRole()
            {
                ModuleId = adminModule.Id,
                RoleId = Admin.Id,
                Add = true,
                Delete = true,
                Update = true,
                View = true,
                CreateUserId = omer.Id,
                CreateUserDate = DateTime.Now,
            };

            ModulesInRole mr2 = new ModulesInRole()
            {
                ModuleId = stockModule.Id,
                RoleId = Admin.Id,
                Add = true,
                Delete = true,
                Update = true,
                View = true,
                CreateUserId = omer.Id,
                CreateUserDate = DateTime.Now,
            };

            ModulesInRole mr3 = new ModulesInRole()
            {
                ModuleId = stockModule.Id,
                RoleId = StokRole.Id,
                Add = true,
                Delete = true,
                Update = true,
                View = true,
                CreateUserId = omer.Id,
                CreateUserDate = DateTime.Now,
            };

            context.ModulesInRoles.Add(mr1);
            context.SaveChanges();
            context.ModulesInRoles.Add(mr2);
            context.SaveChanges();
            context.ModulesInRoles.Add(mr3);
            context.SaveChanges();

            #endregion

            #region Menus
            Menu AdminMenu = new Menu()
            {
                Description = "Yönetim",
                Code = "Admin",
                IsActive = true,
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };

            if (context.Menus.FirstOrDefault(p => p.Code == "Admin") == null)
            {
                context.Menus.Add(AdminMenu);
                context.SaveChanges();
            }

            AdminMenu.Id = context.Menus.FirstOrDefault(p => p.Code == "Admin").Id;

            Menu UsersMenu = new Menu()
            {
                Description = "Kullanıcılar",
                Code = "users",
                IsActive = true,
                ParentId = AdminMenu.Id,
                ModuleId = adminModule.Id,
                CreateDate = DateTime.Today,
                CreateUserId = omer.Id
            };

            Menu Roller = new Menu()
            {
                Description = "Roller",
                Code = "roles",
                IsActive = true,
                ParentId = AdminMenu.Id,
                ModuleId = adminModule.Id,
                CreateDate = DateTime.Today,
                CreateUserId = omer.Id
            };

            Menu ModulesIRoles = new Menu()
            {
                Description = "Modül-Rol",
                Code = "mir",
                IsActive = true,
                ParentId = AdminMenu.Id,
                ModuleId = adminModule.Id,
                CreateDate = DateTime.Today,
                CreateUserId = omer.Id
            };

            Menu Moduller = new Menu()
            {
                Description = "Modüller",
                Code = "modules",
                IsActive = true,
                ParentId = AdminMenu.Id,
                ModuleId = adminModule.Id,
                CreateDate = DateTime.Today,
                CreateUserId = omer.Id
            };

            context.Menus.Add(UsersMenu);
            context.Menus.Add(Roller);
            context.Menus.Add(Moduller);
            context.Menus.Add(ModulesIRoles);

            context.SaveChanges();

            Menu StokMenu = new Menu()
            {
                Description = "Stok",
                Code = "stock",
                IsActive = true,
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today

            };

            if (context.Menus.FirstOrDefault(p => p.Code == "stokc") == null)
            {
                context.Menus.Add(StokMenu);
                context.SaveChanges();
            }
            StokMenu.Id = context.Menus.FirstOrDefault(p => p.Code == "stock").Id;


            Menu stockTypes = new Menu()
            {
                Description = "Stok Tipleri",
                Code = "stocktypes",
                IsActive = true,
                ParentId = StokMenu.Id,
                ModuleId = stockModule.Id,
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };

            Menu stockDefinitions = new Menu()
            {
                Description = "Stok tanımları",
                Code = "stocks",
                IsActive = true,
                ParentId = StokMenu.Id,
                ModuleId = stockModule.Id,
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };

            context.Menus.Add(stockTypes);
            context.Menus.Add(stockDefinitions);

            context.SaveChanges();

            #endregion

            base.Seed(context);
        }

    }
}
