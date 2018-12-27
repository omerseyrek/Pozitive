using SampleArch.Model.Core;
using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleArch.Model
{
    [Table("Project")]
    public partial class Project : Entity<int>
    {
        public Project()
        {
          
        }

        [Required]
        [StringLength(100)]
        [Display(Name = "ProjectName", ResourceType = typeof(Positive.Model.Languages.Project))]
        public string ProjectName { get; set; }

        [NotMapped]
        [StringLength(200)]
        [MultilanguageFieldAttribute("Id")]
        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Project))]
        public string Description { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Positive.Model.Languages.Project))]
        public bool IsActive { get; set; }


        public virtual ICollection<PreRequest> PreRequests { get; set; }
    }
}

