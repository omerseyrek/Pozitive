using SampleArch.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {

        [Required]
        [StringLength(100)]
        [Display(Name = "ProjectName", ResourceType = typeof(Positive.Model.Languages.Project))]
        public string ProjectName { get; set; }

        [StringLength(200)]
        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Project))]
        public string Description { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Positive.Model.Languages.Project))]
        public bool IsActive { get; set; }
    }
}
