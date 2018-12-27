using SampleArch.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{
    public class UsersInRoleViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Account", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Role", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int RoleId { get; set; }

        [Display(Name = "Account", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Account { get; set; }

        [Display(Name = "Role", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string RoleName { get; set; }

        [Display(Name = "RoleCode", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string RoleCode { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    
    }
}
