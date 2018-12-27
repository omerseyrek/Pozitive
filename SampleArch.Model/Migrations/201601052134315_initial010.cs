namespace SampleArch.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial010 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        Key = c.String(),
                        CompleteKey = c.String(),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        SmartCode = c.String(nullable: false, maxLength: 50),
                        CodeIndex = c.String(nullable: false, maxLength: 5),
                        SupplierProductCode = c.String(nullable: false, maxLength: 50),
                        SupplierId = c.Int(nullable: false),
                        StockName = c.String(nullable: false, maxLength: 100),
                        Definition = c.String(nullable: false, maxLength: 500),
                        StockUnitId = c.Int(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.SupplierId)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .ForeignKey("dbo.UnitDefinitions", t => t.StockUnitId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplierId)
                .Index(t => t.StockUnitId)
                .Index(t => t.CreateUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 50, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Password = c.String(nullable: false, maxLength: 20),
                        IsActive = c.Boolean(nullable: false),
                        IsLocked = c.Byte(),
                        TrialCount = c.Short(),
                        UserTitleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTitles", t => t.UserTitleId)
                .Index(t => t.UserTitleId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirmName = c.String(nullable: false, maxLength: 100),
                        TaxNumber = c.String(nullable: false, maxLength: 20),
                        TaxOffice = c.String(nullable: false, maxLength: 30),
                        MersisNo = c.String(maxLength: 50),
                        Note = c.String(maxLength: 500),
                        CreateUserId = c.Int(),
                        CreateDate = c.DateTime(),
                        ModifyUserId = c.Int(),
                        ModifyDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Users", t => t.ModifyUserId)
                .Index(t => t.CreateUserId)
                .Index(t => t.ModifyUserId);
            
            CreateTable(
                "dbo.StockPriceCatalog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        CatalogCode = c.String(nullable: false, maxLength: 20),
                        CreateUserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.SupplierId)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.SupplierId)
                .Index(t => t.CreateUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.StockPrice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatalogId = c.Int(nullable: false),
                        StockId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StockPriceCatalog", t => t.CatalogId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .ForeignKey("dbo.Stocks", t => t.StockId)
                .Index(t => t.CatalogId)
                .Index(t => t.StockId)
                .Index(t => t.CreateUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        ModuleId = c.Int(),
                        Description = c.String(nullable: false, maxLength: 255),
                        Code = c.String(nullable: false, maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        URL = c.String(maxLength: 255),
                        CreateUserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Module", t => t.ModuleId)
                .ForeignKey("dbo.Menu", t => t.ParentId)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.ParentId)
                .Index(t => t.ModuleId)
                .Index(t => t.CreateUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 255),
                        IsActive = c.Boolean(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.CreateUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.ModulesInRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModuleId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        Update = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        View = c.Boolean(nullable: false),
                        Add = c.Boolean(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        CreateUserDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateUserDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Module", t => t.ModuleId)
                .ForeignKey("dbo.Users", t => t.CreateUserId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.ModuleId)
                .Index(t => t.RoleId)
                .Index(t => t.CreateUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsersInRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnitDefinitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreRequestId = c.Int(nullable: false),
                        SmartCode = c.String(nullable: false),
                        SupplierProductCode = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        StockUnitId = c.Int(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId, cascadeDelete: true)
                .ForeignKey("dbo.PreRequest", t => t.PreRequestId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .ForeignKey("dbo.UnitDefinitions", t => t.StockUnitId)
                .Index(t => t.PreRequestId)
                .Index(t => t.StockUnitId)
                .Index(t => t.CreateUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.PreRequest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Description = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                        DeadLineDate = c.DateTime(nullable: false),
                        CreateUserId = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateUserId = c.Int(),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreateUserId, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.ProjectId)
                .ForeignKey("dbo.Users", t => t.UpdateUserId)
                .Index(t => t.ProjectId)
                .Index(t => t.CreateUserId)
                .Index(t => t.UpdateUserId);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MultiLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityType = c.String(),
                        LanguageCode = c.String(),
                        Code = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UILanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LangCode = c.String(maxLength: 10),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "ParentId", "dbo.Category");
            DropForeignKey("dbo.Stocks", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Stocks", "StockUnitId", "dbo.UnitDefinitions");
            DropForeignKey("dbo.RequestLines", "StockUnitId", "dbo.UnitDefinitions");
            DropForeignKey("dbo.RequestLines", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.PreRequest", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.PreRequest", "ProjectId", "dbo.Project");
            DropForeignKey("dbo.RequestLines", "PreRequestId", "dbo.PreRequest");
            DropForeignKey("dbo.PreRequest", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.RequestLines", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.StockPrice", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Users", "UserTitleId", "dbo.UserTitles");
            DropForeignKey("dbo.UsersInRole", "UserID", "dbo.Users");
            DropForeignKey("dbo.StockPrice", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.StockPrice", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.StockPriceCatalog", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.StockPriceCatalog", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Stocks", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Stocks", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.ModulesInRole", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.ModulesInRole", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Module", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Module", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Menu", "UpdateUserId", "dbo.Users");
            DropForeignKey("dbo.Menu", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Menu", "ParentId", "dbo.Menu");
            DropForeignKey("dbo.ModulesInRole", "ModuleId", "dbo.Module");
            DropForeignKey("dbo.UsersInRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.ModulesInRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Menu", "ModuleId", "dbo.Module");
            DropForeignKey("dbo.Companies", "ModifyUserId", "dbo.Users");
            DropForeignKey("dbo.Companies", "CreateUserId", "dbo.Users");
            DropForeignKey("dbo.Stocks", "SupplierId", "dbo.Companies");
            DropForeignKey("dbo.StockPriceCatalog", "SupplierId", "dbo.Companies");
            DropForeignKey("dbo.StockPrice", "CatalogId", "dbo.StockPriceCatalog");
            DropIndex("dbo.PreRequest", new[] { "UpdateUserId" });
            DropIndex("dbo.PreRequest", new[] { "CreateUserId" });
            DropIndex("dbo.PreRequest", new[] { "ProjectId" });
            DropIndex("dbo.RequestLines", new[] { "UpdateUserId" });
            DropIndex("dbo.RequestLines", new[] { "CreateUserId" });
            DropIndex("dbo.RequestLines", new[] { "StockUnitId" });
            DropIndex("dbo.RequestLines", new[] { "PreRequestId" });
            DropIndex("dbo.UsersInRole", new[] { "RoleId" });
            DropIndex("dbo.UsersInRole", new[] { "UserID" });
            DropIndex("dbo.ModulesInRole", new[] { "UpdateUserId" });
            DropIndex("dbo.ModulesInRole", new[] { "CreateUserId" });
            DropIndex("dbo.ModulesInRole", new[] { "RoleId" });
            DropIndex("dbo.ModulesInRole", new[] { "ModuleId" });
            DropIndex("dbo.Module", new[] { "UpdateUserId" });
            DropIndex("dbo.Module", new[] { "CreateUserId" });
            DropIndex("dbo.Menu", new[] { "UpdateUserId" });
            DropIndex("dbo.Menu", new[] { "CreateUserId" });
            DropIndex("dbo.Menu", new[] { "ModuleId" });
            DropIndex("dbo.Menu", new[] { "ParentId" });
            DropIndex("dbo.StockPrice", new[] { "UpdateUserId" });
            DropIndex("dbo.StockPrice", new[] { "CreateUserId" });
            DropIndex("dbo.StockPrice", new[] { "StockId" });
            DropIndex("dbo.StockPrice", new[] { "CatalogId" });
            DropIndex("dbo.StockPriceCatalog", new[] { "UpdateUserId" });
            DropIndex("dbo.StockPriceCatalog", new[] { "CreateUserId" });
            DropIndex("dbo.StockPriceCatalog", new[] { "SupplierId" });
            DropIndex("dbo.Companies", new[] { "ModifyUserId" });
            DropIndex("dbo.Companies", new[] { "CreateUserId" });
            DropIndex("dbo.Users", new[] { "UserTitleId" });
            DropIndex("dbo.Stocks", new[] { "UpdateUserId" });
            DropIndex("dbo.Stocks", new[] { "CreateUserId" });
            DropIndex("dbo.Stocks", new[] { "StockUnitId" });
            DropIndex("dbo.Stocks", new[] { "SupplierId" });
            DropIndex("dbo.Stocks", new[] { "CategoryId" });
            DropIndex("dbo.Category", new[] { "ParentId" });
            DropTable("dbo.UILanguages");
            DropTable("dbo.MultiLanguages");
            DropTable("dbo.Project");
            DropTable("dbo.PreRequest");
            DropTable("dbo.RequestLines");
            DropTable("dbo.UnitDefinitions");
            DropTable("dbo.UserTitles");
            DropTable("dbo.UsersInRole");
            DropTable("dbo.Roles");
            DropTable("dbo.ModulesInRole");
            DropTable("dbo.Module");
            DropTable("dbo.Menu");
            DropTable("dbo.StockPrice");
            DropTable("dbo.StockPriceCatalog");
            DropTable("dbo.Companies");
            DropTable("dbo.Users");
            DropTable("dbo.Stocks");
            DropTable("dbo.Category");
        }
    }
}
