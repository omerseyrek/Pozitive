using SampleArch.Model.Initialize;
using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleArch.Model
{
    public class SampleArchContext : DbContext
    {

        public SampleArchContext()
            : base("Name=SampleArchContext")
        {}

        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<ModulesInRole> ModulesInRoles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersInRole> UsersInRoles { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<MultiLanguage> MultiLanguage { get; set; }
        public virtual DbSet<UILanguage> UILanguage { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<UnitDefinition> UnitDefinitions { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<UserTitle> UserTitles { get; set; }


        public virtual DbSet<StockPriceCatalog> StockPriceCatalogs { get; set; }
        public virtual DbSet<StockPrice> StockPrices { get; set; }

        public virtual DbSet<PreRequest> PreRequest { get; set; }
        public virtual DbSet<RequestLine> RequestLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Role>().HasKey(e => e.Id);
            modelBuilder.Entity<Role>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Menu>().HasKey(e => e.Id);
            modelBuilder.Entity<Menu>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Module>().HasKey(e => e.Id);
            modelBuilder.Entity<Module>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ModulesInRole>().HasKey(e => e.Id);
            modelBuilder.Entity<ModulesInRole>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<UsersInRole>().HasKey(e => e.Id);
            modelBuilder.Entity<UsersInRole>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Company>().HasKey(e => e.Id);
            modelBuilder.Entity<Company>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<MultiLanguage>().HasKey(e => e.Id);
            modelBuilder.Entity<MultiLanguage>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<UILanguage>().HasKey(e => e.Id);
            modelBuilder.Entity<UILanguage>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<Category>().HasKey(e => e.Id);
            modelBuilder.Entity<Category>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<UnitDefinition>().HasKey(e => e.Id);
            modelBuilder.Entity<UnitDefinition>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<Stock>().HasKey(e => e.Id);
            modelBuilder.Entity<Stock>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<Project>().HasKey(e => e.Id);
            modelBuilder.Entity<Project>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<UserTitle>().HasKey(e => e.Id);
            modelBuilder.Entity<UserTitle>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<StockPrice>().HasKey(e => e.Id);
            modelBuilder.Entity<StockPrice>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<StockPriceCatalog>().HasKey(e => e.Id);
            modelBuilder.Entity<StockPriceCatalog>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<PreRequest>().HasKey(e => e.Id);
            modelBuilder.Entity<PreRequest>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<RequestLine>().HasKey(e => e.Id);
            modelBuilder.Entity<RequestLine>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<StockImage>().HasKey(e => e.Id);
            modelBuilder.Entity<StockImage>().Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            modelBuilder.Entity<Category>()
               .HasMany(e => e.SubCategory)
               .WithOptional(e => e.Parent)
               .HasForeignKey(e => e.ParentId);


            modelBuilder.Entity<Menu>()
               .HasMany(e => e.SubMenu)
               .WithOptional(e => e.Parent)
               .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.ModulesInRoles)
                .WithRequired(e => e.Module)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.ModulesInRoles)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UsersInRoles)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Menu)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.CreateUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Menu1)
                .WithOptional(e => e.Users1)
                .HasForeignKey(e => e.UpdateUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Module)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.CreateUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Module1)
                .WithOptional(e => e.Users1)
                .HasForeignKey(e => e.UpdateUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ModulesInRoles)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.CreateUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ModulesInRoles1)
                .WithOptional(e => e.Users1)
                .HasForeignKey(e => e.UpdateUserId);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UsersInRoles)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CompanyCreatred)
                .WithOptional(e => e.CreateUser)
                .HasForeignKey(e => e.CreateUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CompanyModified)
                .WithOptional(e => e.ModifyUser)
                .HasForeignKey(e => e.ModifyUserId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<User>()
                 .HasMany(e => e.StockCreatred)
                 .WithRequired(e => e.CreateUser)
                 .HasForeignKey(e => e.CreateUserId)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                 .HasMany(e => e.StockModified)
                 .WithOptional(e => e.UpdateUser)
                 .HasForeignKey(e => e.UpdateUserId)
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<User>()
                 .HasMany(e => e.StockPriceCreatred)
                 .WithRequired(e => e.CreateUser)
                 .HasForeignKey(e => e.CreateUserId)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                 .HasMany(e => e.StockPriceModified)
                 .WithOptional(e => e.UpdateUser)
                 .HasForeignKey(e => e.UpdateUserId)
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<User>()
                 .HasMany(e => e.StockPriceCategoryCreatred)
                 .WithRequired(e => e.CreateUser)
                 .HasForeignKey(e => e.CreateUserId)
                 .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                 .HasMany(e => e.StockPriceCategoryModified)
                 .WithOptional(e => e.UpdateUser)
                 .HasForeignKey(e => e.UpdateUserId)
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<Company>()
                 .HasMany(e => e.Stocks)
                 .WithRequired(e => e.SupplierCompany)
                 .HasForeignKey(e => e.SupplierId)
                 .WillCascadeOnDelete(false);


            modelBuilder.Entity<Company>()
                 .HasMany(e => e.StockPriceCatalogs)
                 .WithRequired(e => e.SupplierCompany)
                 .HasForeignKey(e => e.SupplierId)
                 .WillCascadeOnDelete(false);



            modelBuilder.Entity<Category>()
                  .HasMany(e => e.Stocks)
                  .WithRequired(e => e.Category)
                  .HasForeignKey(e => e.CategoryId)
                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<UnitDefinition>()
              .HasMany(e => e.StockUnits)
              .WithRequired(e => e.UnitDefinition)
              .HasForeignKey(e => e.StockUnitId)
              .WillCascadeOnDelete(false);


            modelBuilder.Entity<UserTitle>()
              .HasMany(e => e.UsersInTitle)
              .WithRequired(e => e.UserTitle)
              .HasForeignKey(e => e.UserTitleId)
              .WillCascadeOnDelete(false);


            //stockprica catalog - stockprices
            modelBuilder.Entity<StockPriceCatalog>()
                  .HasMany(e => e.Prices)
                  .WithRequired(e => e.Catalog)
                  .HasForeignKey(e => e.CatalogId)
                  .WillCascadeOnDelete(true);


            //stockprica  - stock
            modelBuilder.Entity<Stock>()
                  .HasMany(e => e.Prices)
                  .WithRequired(e => e.StockItem)
                  .HasForeignKey(e => e.StockId)
                  .WillCascadeOnDelete(false);


            //stock image  - stock to do : later make ths join as 1 to 1 correspondence
            modelBuilder.Entity<Stock>()
                  .HasMany(e => e.StockImages)
                  .WithRequired(e => e.Stock)
                  .HasForeignKey(e => e.StockId)
                  .WillCascadeOnDelete(false);

            //prerequest  - project
            modelBuilder.Entity<Project>()
                  .HasMany(e => e.PreRequests)
                  .WithRequired(e => e.Project)
                  .HasForeignKey(e => e.ProjectId)
                  .WillCascadeOnDelete(false);


            //pre request - line
            modelBuilder.Entity<PreRequest>()
                  .HasMany(e => e.Items)
                  .WithRequired(e => e.TheRequest)
                  .HasForeignKey(e => e.PreRequestId)
                  .WillCascadeOnDelete(false);

            // pre request line - unit
            modelBuilder.Entity<UnitDefinition>()
               .HasMany(e => e.RequestLines)
               .WithRequired(e => e.UnitDefinition)
               .HasForeignKey(e => e.StockUnitId)
               .WillCascadeOnDelete(false);
        }
    }    
}
