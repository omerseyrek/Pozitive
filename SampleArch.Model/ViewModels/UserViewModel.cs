using SampleArch.Model.Core;
using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{

    public class UserViewModel : BaseViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Account", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Account { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(Positive.Model.Languages.Admin))]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Email", ResourceType = typeof(Positive.Model.Languages.Admin))]
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

        [Display(Name = "TrialCount", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public short? TrialCount { get; set; }

        [Required]
        [Display(Name = "UserTitleId", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int UserTitleId { get; set; }

        [Display(Name = "UserTitleId", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public UserTitle UserTitle { get; set; }


        [Display(Name = "UserTitleId", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string UserTitleText { get; set; }
    }
}
