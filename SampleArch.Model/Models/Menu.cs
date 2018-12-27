
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace SampleArch.Model
{
    [Table("Menu")]
    public partial class Menu : Entity<int>
    {

        public Menu()
        {
            SubMenu = new List<Menu>();
        }

        [Display(Name = "ParentId", ResourceType = typeof(Positive.Model.Languages.Common))]
        public int? ParentId { get; set; }

        [Display(Name = "Module", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int? ModuleId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Code", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Code { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool IsActive { get; set; }

        [StringLength(255)]
        public string URL { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual IList<Menu> SubMenu { get; set; }

        public virtual Menu Parent { get; set; }

        public virtual User Users { get; set; }

        public virtual User Users1 { get; set; }

        public virtual Module Module { get; set; }


    }
}
