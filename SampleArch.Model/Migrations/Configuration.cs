namespace SampleArch.Model.Migrations
{
    using SampleArch.Model.Core;
    using SampleArch.Model.Initialize;
    using SampleArch.Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<SampleArch.Model.SampleArchContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SampleArch.Model.SampleArchContext";

        }

        public void LeSeed(SampleArch.Model.SampleArchContext context)
        {
            if (context.Database.Exists() == false)
            {
                context.Database.CreateIfNotExists();
                Seed(context);
            }
        }

        protected override void Seed(SampleArch.Model.SampleArchContext context)
        {
            #region Addlanguages
            UILanguage Turkish = new UILanguage()
            {
                LangCode = "tr",
                Description = "Türkçe"
            };

            context.UILanguage.Add(Turkish);

            UILanguage English = new UILanguage()
            {
                LangCode = "en",
                Description = "İngilizce"
            };

            context.UILanguage.Add(English);

            context.SaveChanges();

            #endregion

            #region Titles 
            UserTitle purchase = new UserTitle();
            purchase.Code = "PUR";

            if (context.UserTitles.FirstOrDefault(p => p.Code == purchase.Code) == null)
            {
                context.UserTitles.Add(purchase);
                
            }

            MultiLanguage mltitleTr = new MultiLanguage()
            {
                Code = purchase.Code,
                EntityType = purchase.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.TR.ToString(),
                Value = "Satınalma"
            };

            MultiLanguage mltitleEn = new MultiLanguage()
            {
                Code = purchase.Code,
                EntityType = purchase.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.EN.ToString(),
                Value = "Purchase"
            };

            context.MultiLanguage.Add(mltitleTr);
            context.MultiLanguage.Add(mltitleEn);

            context.SaveChanges();
            #endregion


            #region Add Users
            User omer = new User()
            {
                Name = "ömer",
                LastName = "seyrek",
                Account = "oseyrek",
                Password = "123456",
                Email = "omerseyrek@hotmail.com",
                UserTitleId = purchase.Id,
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
                Account = "edemirer",
                Password = "123456",
                Email = "ermandemirer@hotmail.com",
                UserTitleId = purchase.Id,
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
                UserTitleId = purchase.Id,
                IsActive = true
            };

            if (context.Users.FirstOrDefault(p => p.Account == "tolgat") == null)
            {
                context.Users.Add(tolga);
                context.SaveChanges();
            }

            tolga.Id = context.Users.FirstOrDefault(p => p.Account == "tolgat").Id;


            User Ercan = new User()
            {
                Name = "ercan",
                LastName = "kilic",
                Account = "ercank",
                Password = "123456",
                Email = "ercank@ercank.com",
                UserTitleId = purchase.Id,
                IsActive = true
            };

            if (context.Users.FirstOrDefault(p => p.Account == "ercank") == null)
            {
                context.Users.Add(Ercan);
                context.SaveChanges();
            }

            Ercan.Id = context.Users.FirstOrDefault(p => p.Account == "ercank").Id;
            #endregion


            #region Add Roles
            Role Admin = new Role()
            {
                Code = "admin",
            };

            MultiLanguage mlAdminTr = new MultiLanguage()
            {
                Code = Admin.Code,
                EntityType = Admin.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.TR.ToString(),
                Value = "Yönetim"
            };

            MultiLanguage mlAdminEn = new MultiLanguage()
            {
                Code = Admin.Code,
                EntityType = Admin.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.EN.ToString(),
                Value = "Administrative"
            };

            context.MultiLanguage.Add(mlAdminTr);
            context.MultiLanguage.Add(mlAdminEn);


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

             MultiLanguage mlStockTr = new MultiLanguage()
            {
                Code = StokRole.Code,
                EntityType = StokRole.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.TR.ToString(),
                Value = "Stok"
            };

            MultiLanguage mlStockEn = new MultiLanguage()
            {
                Code = StokRole.Code,
                EntityType = StokRole.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.EN.ToString(),
                Value = "Stock"
            };

            context.MultiLanguage.Add(mlStockTr);
            context.MultiLanguage.Add(mlStockEn);

            if (context.Roles.FirstOrDefault(p => p.Code == "stock") == null)
            {
                context.Roles.Add(StokRole);
                context.SaveChanges();
            }
            StokRole.Id = context.Roles.FirstOrDefault(p => p.Code == "stock").Id;


            Role crmRole = new Role()
            {
                Code = "crm",
            };

             MultiLanguage mlCRMTr = new MultiLanguage()
            {
                Code = crmRole.Code,
                EntityType = crmRole.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.TR.ToString(),
                Value = "Crm"
            };

            MultiLanguage mlCRMEn = new MultiLanguage()
            {
                Code = crmRole.Code,
                EntityType = crmRole.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.EN.ToString(),
                Value = "Crm"
            };

            context.MultiLanguage.Add(mlCRMEn);
            context.MultiLanguage.Add(mlCRMTr);

            if (context.Roles.FirstOrDefault(p => p.Code == "crm") == null)
            {
                context.Roles.Add(crmRole);
                context.SaveChanges();
            }
            crmRole.Id = context.Roles.FirstOrDefault(p => p.Code == "crm").Id;

            context.SaveChanges();



            Role PreRequestRole = new Role()
            {
                Code = "prerequest",
            };

            MultiLanguage mlPreRequestTr = new MultiLanguage()
            {
                Code = PreRequestRole.Code,
                EntityType = Admin.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.TR.ToString(),
                Value = "Ön Talep"
            };

            MultiLanguage mlPreRequestEn = new MultiLanguage()
            {
                Code = PreRequestRole.Code,
                EntityType = Admin.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.EN.ToString(),
                Value = "Pre Request"
            };

            context.MultiLanguage.Add(mlPreRequestTr);
            context.MultiLanguage.Add(mlPreRequestEn);


            if (context.Roles.FirstOrDefault(p => p.Code == "prerequest") == null)
            {
                context.Roles.Add(PreRequestRole);
                context.SaveChanges();
            }

            PreRequestRole.Id = context.Roles.FirstOrDefault(p => p.Code == "prerequest").Id;
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


            UsersInRole r4 = new UsersInRole()
            {
                UserID = Ercan.Id,
                RoleId = crmRole.Id
            };
            if (context.UsersInRoles.FirstOrDefault(p => p.UserID == omer.Id && p.RoleId == crmRole.Id) == null)
            {
                context.UsersInRoles.Add(r4);
            }

            UsersInRole r5 = new UsersInRole()
            {
                UserID = Ercan.Id,
                RoleId = Admin.Id
            };
            if (context.UsersInRoles.FirstOrDefault(p => p.UserID == Ercan.Id && p.RoleId == Admin.Id) == null)
            {
                context.UsersInRoles.Add(r5);
            }


            UsersInRole r6 = new UsersInRole()
            {
                UserID = erman.Id,
                RoleId = PreRequestRole.Id
            };
            if (context.UsersInRoles.FirstOrDefault(p => p.UserID == erman.Id && p.RoleId == PreRequestRole.Id) == null)
            {
                context.UsersInRoles.Add(r6);
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

            Module CRMModule = new Module()
            {
                Description = "CRM",
                Code = "crm",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Now,
            };
            if (context.Modules.FirstOrDefault(p => p.Code == "crm") == null)
            {
                context.Modules.Add(CRMModule);
                context.SaveChanges();
            }
            CRMModule.Id = context.Modules.FirstOrDefault(p => p.Code == "crm").Id;



            Module PreRequestModule = new Module()
            {
                Description = "Ön Talep",
                Code = "pre",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Now,
            };
            if (context.Modules.FirstOrDefault(p => p.Code == "pre") == null)
            {
                context.Modules.Add(PreRequestModule);
                context.SaveChanges();
            }
            PreRequestModule.Id = context.Modules.FirstOrDefault(p => p.Code == "pre").Id;


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


            ModulesInRole mr4 = new ModulesInRole()
            {
                ModuleId = CRMModule.Id,
                RoleId = crmRole.Id,
                Add = true,
                Delete = true,
                Update = true,
                View = true,
                CreateUserId = omer.Id,
                CreateUserDate = DateTime.Now,
            };

            ModulesInRole mr5 = new ModulesInRole()
            {
                ModuleId = PreRequestModule.Id,
                RoleId = PreRequestRole.Id,
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
            context.ModulesInRoles.Add(mr4);
            context.SaveChanges();
            context.ModulesInRoles.Add(mr5);
            context.SaveChanges();

            #endregion


            #region Menus
            Menu AdminMenu = new Menu()
            {
                Description = "Yönetim",
                Code = "admin",
                IsActive = true,
                URL = "",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };

            if (context.Menus.FirstOrDefault(p => p.Code == "admin") == null)
            {
                context.Menus.Add(AdminMenu);
                context.SaveChanges();
            }

            AdminMenu.Id = context.Menus.FirstOrDefault(p => p.Code == "admin").Id;

            Menu UsersMenu = new Menu()
            {
                Description = "Kullanıcılar",
                Code = "users",
                IsActive = true,
                ParentId = AdminMenu.Id,
                ModuleId = adminModule.Id,
                URL =   "/User/Index",
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
                URL =  "/Role/Index",
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
                URL = "/ModulesInRole/Index",
                CreateDate = DateTime.Today,
                CreateUserId = omer.Id
            };


            Menu UserInRole = new Menu()
            {
                Description = "Kullanıcı-Rol",
                Code = "uir",
                IsActive = true,
                ParentId = AdminMenu.Id,
                ModuleId = adminModule.Id,
                URL = "/UsersInRole/Index",
                CreateDate = DateTime.Today,
                CreateUserId = omer.Id
            };

            Menu Projects = new Menu()
            {
                Description = "Projects",
                Code = "projects",
                IsActive = true,
                ParentId = AdminMenu.Id,
                ModuleId = adminModule.Id,
                URL = "/Project/Index",
                CreateDate = DateTime.Today,
                CreateUserId = omer.Id
            };


            Menu UserTitles = new Menu()
            {
                Description = "UserTitles",
                Code = "usertitles",
                IsActive = true,
                ParentId = AdminMenu.Id,
                ModuleId = adminModule.Id,
                URL = "/UserTitle/Index",
                CreateDate = DateTime.Today,
                CreateUserId = omer.Id
            };

            context.Menus.Add(UserInRole);
            context.Menus.Add(UsersMenu);
            context.Menus.Add(Roller);
            context.Menus.Add(ModulesIRoles);
            context.Menus.Add(Projects);
            context.Menus.Add(UserTitles);

            context.SaveChanges();

            Menu StokMenu = new Menu()
            {
                Description = "Stok",
                Code = "stock",
                IsActive = true,
                URL = "",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today

            };

            if (context.Menus.FirstOrDefault(p => p.Code == "stock") == null)
            {
                context.Menus.Add(StokMenu);
                context.SaveChanges();
            }
            StokMenu.Id = context.Menus.FirstOrDefault(p => p.Code == "stock").Id;


            Menu stockTypes = new Menu()
            {
                Description = "Stok Birimleri",
                Code = "stockunits",
                IsActive = true,
                ParentId = StokMenu.Id,
                ModuleId = stockModule.Id,
                URL =  "/UnitDefinitions/Index",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };

          

            Menu stockCategories = new Menu()
            {
                Description = "Kategoriler",
                Code = "category",
                IsActive = true,
                ParentId = StokMenu.Id,
                ModuleId = stockModule.Id,
                URL =  "/Category/Index",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };


            Menu stockDefinitions = new Menu()
            {
                Description = "Stok Tanımları",
                Code = "stockdefinitions",
                IsActive = true,
                ParentId = StokMenu.Id,
                ModuleId = stockModule.Id,
                URL =  "/Stock/Index",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };


            Menu stockPriceCatalog = new Menu()
            {
                Description = "Stok Fiyat Katalog",
                Code = "stockpricecatalog",
                IsActive = true,
                ParentId = StokMenu.Id,
                ModuleId = stockModule.Id,
                URL = "/StockPriceCatalog/Index",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };

            context.Menus.Add(stockCategories);
            context.Menus.Add(stockTypes);
            context.Menus.Add(stockDefinitions);
            context.Menus.Add(stockPriceCatalog);

            Menu CRMMenu = new Menu()
            {
                Description = "CRM",
                Code = "crm",
                IsActive = true,
                URL = "",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };

            if (context.Menus.FirstOrDefault(p => p.Code == "crm") == null)
            {
                context.Menus.Add(CRMMenu);
                context.SaveChanges();
            }

            CRMMenu.Id = context.Menus.FirstOrDefault(p => p.Code == "crm").Id;

            Menu companiesMenu = new Menu()
            {
                Description = "Firmalar",
                Code = "firms",
                IsActive = true,
                ParentId = CRMMenu.Id,
                ModuleId = CRMModule.Id,
                URL = "/Company/Index",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };

            context.Menus.Add(companiesMenu);

            context.SaveChanges();

            #endregion

            #region CRM

            Company cABB = new Company()
            {
                CreateDate = DateTime.Now,
                CreateUserId = omer.Id,
                FirmName = "ABB",
                MersisNo = "111-222-333",
                Note = "ABB hakkında ek bilgiler...",
                TaxNumber = "3800004838",
                TaxOffice = "Merter"
            };


            Company cViko = new Company()
            {
                CreateDate = DateTime.Now,
                CreateUserId = omer.Id,
                FirmName = "VİKO",
                MersisNo = "111-222-333",
                Note = "VİKO hakkında ek bilgiler...",
                TaxNumber = "2540005478",
                TaxOffice = "Kızıltoprak"
            };

            context.Companies.Add(cABB);
            context.Companies.Add(cViko);
            context.SaveChanges();

            #endregion

            #region categories add stocks

            UnitDefinition unit = new UnitDefinition()
            {
                Code = "Q",
                Description = "Adet"
            };

            MultiLanguage unitMLTr = new MultiLanguage()
            {
                Code = unit.Code,
                EntityType = unit.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.TR.ToString(),
                Value = "Adet"
            };

            MultiLanguage unitMLEn = new MultiLanguage()
            {
                Code = unit.Code,
                EntityType = unit.GetType().ToString(),
                LanguageCode = EnumDefinitions.Language.TR.ToString(),
                Value = "Adet"
            };


            context.UnitDefinitions.Add(unit);
            context.MultiLanguage.Add(unitMLTr);
            context.MultiLanguage.Add(unitMLEn);

            context.SaveChanges();


            Category PLS = new Category()
            {
                CompleteKey = "PLS",
                Description = "Plastik Malzemeler",
                Key = "PLS",
                Name = "Plastik Malzemeler",
                ParentId = null
            };


            if (context.Categories.FirstOrDefault(p => p.CompleteKey == PLS.CompleteKey) == null)
            {
                context.Categories.Add(PLS);
                context.SaveChanges();
            }
            PLS.Id = context.Categories.FirstOrDefault(p => p.CompleteKey == PLS.CompleteKey).Id;

            Category OUT = new Category()
            {
                Description = "Dış Cephe Malzemeler",
                Key = "OUT",
                CompleteKey = "PLS.OUT",
                Name = "Dış Cephe Malzemeler",
                ParentId = PLS.Id
            };

            if (context.Categories.FirstOrDefault(p => p.CompleteKey == OUT.CompleteKey) == null)
            {
                context.Categories.Add(OUT);
                context.SaveChanges();
            }
            OUT.Id = context.Categories.FirstOrDefault(p => p.CompleteKey == OUT.CompleteKey).Id;


            Category OLK = new Category()
            {
                Description = "Yağmur olukları",
                Key = "OLK",
                CompleteKey = "PLS.OUT.OLK",
                Name = "plastik yağmur olukları",
                ParentId = OUT.Id
            };

            if (context.Categories.FirstOrDefault(p => p.CompleteKey == OLK.CompleteKey) == null)
            {
                context.Categories.Add(OLK);
                context.SaveChanges();
            }
            OLK.Id = context.Categories.FirstOrDefault(p => p.CompleteKey == OLK.CompleteKey).Id;


            #endregion

            #region Pre Request
            Menu PreRequestMaster = new Menu()
            {
                Description = "Ön Talep",
                Code = "prerequest",
                IsActive = true,
                URL = "",
                CreateUserId = omer.Id,
                CreateDate = DateTime.Today
            };

            if (context.Menus.FirstOrDefault(p => p.Code == "prerequest") == null)
            {
                context.Menus.Add(PreRequestMaster);
                context.SaveChanges();
            }

            PreRequestMaster.Id = context.Menus.FirstOrDefault(p => p.Code == "prerequest").Id;

            Menu PreRequestMenu = new Menu()
            {
                Description = "Ön Talep",
                Code = "prerequestentry",
                IsActive = true,
                ParentId = PreRequestMaster.Id,
                ModuleId = PreRequestModule.Id,
                URL = "/PreRequest/Index",
                CreateDate = DateTime.Today,
                CreateUserId = omer.Id
            };

            context.Menus.Add(PreRequestMenu);

            context.SaveChanges();
            #endregion
            base.Seed(context);
        }
    }
}
