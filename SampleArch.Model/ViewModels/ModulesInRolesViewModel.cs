using SampleArch.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{

    public class ModulesInRolesViewModel : BaseViewModel
    {
        #region Properties
        [Required]
        [Display(Name = "ModuleName", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int ModuleId { get; set; }

        [Display(Name = "ModuleName", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string ModuleName { get; set; }

        [Display(Name = "ModuleCode", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string ModuleCode { get; set; }

        [Display(Name = "Role", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int RoleId { get; set; }

        [Required]
        [Display(Name = "Role", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string RoleName { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }

        [Display(Name = "Update", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool Update { get; set; }

        [Display(Name = "Delete", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool Delete { get; set; }

        [Display(Name = "View", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool View { get; set; }

        [Display(Name = "Add", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool Add { get; set; }

        #endregion

    }
}
