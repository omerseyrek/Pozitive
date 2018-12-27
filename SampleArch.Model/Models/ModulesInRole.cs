
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
namespace SampleArch.Model
{
    [Table("ModulesInRole")]
    public partial class ModulesInRole : Entity<int>
    {
        [Display(Name = "Module", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int ModuleId { get; set; }

        [Display(Name = "Role", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int RoleId { get; set; }

        [Display(Name = "Update", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool Update { get; set; }

        [Display(Name = "Delete", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool Delete { get; set; }

        [Display(Name = "View", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool View { get; set; }

         [Display(Name = "Add", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool Add { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateUserDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateUserDate { get; set; }

        public virtual Module Module { get; set; }

        public virtual User Users { get; set; }

        public virtual User Users1 { get; set; }

        public virtual Role Roles { get; set; }
    }
}
