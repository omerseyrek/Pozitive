using SampleArch.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Models
{
    public class UserTitle :  Entity<int>
    {
        public UserTitle()
        {
            UsersInTitle = new HashSet<User>();
        }

        [Required]
        [StringLength(100)]
        [Display(Name = "Code", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Code { get; set; }

        [StringLength(200)]
        [NotMapped]
        [MultilanguageFieldAttribute("Code")]
        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }


        public virtual ICollection<User> UsersInTitle { get; set; }
    }
}
