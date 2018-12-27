using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Models
{

    [Table("PreRequest")]
    public partial class PreRequest : Entity<int>
    {
        public PreRequest()
        {

        }

        [Required]
        [Display(Name = "ProjectName", ResourceType = typeof(Positive.Model.Languages.Project))]
        public int ProjectId { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }

        [Required]
        [Display(Name = "RequestDate", ResourceType = typeof(Positive.Model.Languages.Request))]
        public DateTime RequestDate { get; set; }


        [Required]
        [Display(Name = "DeadLineDate", ResourceType = typeof(Positive.Model.Languages.Request))]
        public DateTime DeadLineDate { get; set; }

        [Required]
        public int CreateUserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual User CreateUser { get; set; }

        public virtual User UpdateUser { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<RequestLine> Items { get; set; }
    }
}
