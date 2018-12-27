using SampleArch.Model.Core;
using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleArch.Model
{

    public partial class User : Entity<int>
    {
        public User()
        {
            Menu = new HashSet<Menu>();
            Menu1 = new HashSet<Menu>();
            Module = new HashSet<Module>();
            Module1 = new HashSet<Module>();
            ModulesInRoles = new HashSet<ModulesInRole>();
            ModulesInRoles1 = new HashSet<ModulesInRole>();
            UsersInRoles = new HashSet<UsersInRole>();
            CompanyCreatred = new HashSet<Company>();
            CompanyModified = new HashSet<Company>();
            StockCreatred = new HashSet<Stock>();
            StockModified = new HashSet<Stock>();
        }


        [Required]
        [StringLength(50)]
        [Display(Name = "Account", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Account { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Name", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Mail", ResourceType = typeof(Positive.Model.Languages.Admin))]
        [EmailAddress(ErrorMessage = "",  ErrorMessageResourceName = "Mail",  ErrorMessageResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Email { get; set; }


        [Required]
        [StringLength(10)]
        [Display(Name = "LastName", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Password", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Password { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public bool IsActive { get; set; }

        [Display(Name = "IsLocked", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public byte? IsLocked { get; set; }

        public short? TrialCount { get; set; }

        [Required]
        [Display(Name = "UserTitleId", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int UserTitleId { get; set; }

        [MultilanguageEntityAttribute("UserTitleId")]
        public virtual UserTitle UserTitle { get; set; }

        public virtual ICollection<Menu> Menu { get; set; }

        public virtual ICollection<Menu> Menu1 { get; set; }

        public virtual ICollection<Module> Module { get; set; }

        public virtual ICollection<Module> Module1 { get; set; }

        public virtual ICollection<ModulesInRole> ModulesInRoles { get; set; }

        public virtual ICollection<ModulesInRole> ModulesInRoles1 { get; set; }

        public virtual ICollection<UsersInRole> UsersInRoles { get; set; }

        public virtual ICollection<Company> CompanyCreatred { get; set; }

        public virtual ICollection<Company> CompanyModified { get; set; }

        public virtual ICollection<Stock> StockCreatred { get; set; }

        public virtual ICollection<Stock> StockModified { get; set; }

        public virtual ICollection<StockPrice> StockPriceCreatred { get; set; }

        public virtual ICollection<StockPrice> StockPriceModified { get; set; }

        public virtual ICollection<StockPriceCatalog> StockPriceCategoryCreatred { get; set; }

        public virtual ICollection<StockPriceCatalog> StockPriceCategoryModified { get; set; }

    }
}
