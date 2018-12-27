using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SampleArch.Model
{
     [Table("UsersInRole")]
    public partial class UsersInRole : Entity<int>
    {

        [Required]
        [Display(Name = "Account", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Role", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int RoleId { get; set; }

        public virtual Role Roles { get; set; }

        public virtual User Users { get; set; }
    }
}
