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
    public class PreRequestViewModel : BaseViewModel
    {

        [Required]
        [Display(Name = "ProjectName", ResourceType = typeof(Positive.Model.Languages.Project))]
        public int ProjectId { get; set; }

        [Display(Name = "ProjectName", ResourceType = typeof(Positive.Model.Languages.Project))]
        public string ProjectName { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }

        [Required]
        [Display(Name = "RequestDate", ResourceType = typeof(Positive.Model.Languages.Request))]
        public DateTime RequestDate { get; set; }


        [Required]
        [Display(Name = "DeadLineDate", ResourceType = typeof(Positive.Model.Languages.Request))]
        public DateTime DeadLineDate { get; set; }


        public virtual ProjectViewModel Project { get; set; }

        public virtual ICollection<RequestLineViewModel> RequestLines { get; set; }

        public virtual IEnumerable<int> DestroyedIDs { get; set; }
    }


    
}
