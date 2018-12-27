using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{

    public class UserTitleViewModel : BaseViewModel
    {

        [Required]
        [StringLength(5)]
        public virtual string Code { get; set; }


        [Required]
        [MultilanguageFieldAttribute("Code")]
        public virtual string Description { get; set; }

        

    }
}
