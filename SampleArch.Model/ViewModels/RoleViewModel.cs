
using SampleArch.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{

    public class RoleViewModel  : BaseViewModel
    {
   
        [Required]
        [StringLength(50)]
        [Display(Name = "RoleCode", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public virtual string Code { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public virtual string Description { get; set; }

        public List<ValidationResult> validations { get; set; }
    }
}
