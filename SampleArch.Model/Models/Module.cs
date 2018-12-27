
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleArch.Model
{
    [Table("Module")]
    public partial class Module : Entity<int>
    {
        public Module()
        {
            Menu = new HashSet<Menu>();
            ModulesInRoles = new HashSet<ModulesInRole>();
        }

        [Required]
        [StringLength(255)]
        [Display(Name = "ModuleCode", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }

         [Display(Name = "IsActive", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool IsActive { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Menu> Menu { get; set; }

        public virtual User Users { get; set; }

        public virtual User Users1 { get; set; }

        public virtual ICollection<ModulesInRole> ModulesInRoles { get; set; }

    }
}
