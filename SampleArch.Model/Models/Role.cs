
using SampleArch.Model.Core;
using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SampleArch.Model
{

    public partial class Role : Entity<int>
    {
        public Role()
        {
            ModulesInRoles = new HashSet<ModulesInRole>();
            UsersInRoles = new HashSet<UsersInRole>();
        }


        [StringLength(50)]
        [Required]
        [Display(Name = "RoleCode", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Code { get; set; }

        [NotMapped]
        [MultilanguageFieldAttribute("Code")]
        [Display(Name = "Role", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Description { get; set; }

        public virtual ICollection<ModulesInRole> ModulesInRoles { get; set; }

        public virtual ICollection<UsersInRole> UsersInRoles { get; set; }

    }
}
