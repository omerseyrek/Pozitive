using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels.Admin
{
    public  class LogonViewModel
    {
        [Required]
        [Display(Name = "Account", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public bool RememberMe { get; set; }

        [Required]
        [Display(Name = "LanguageCode", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string LanguageCode { get; set; }

        public List<UILanguageViewModel> LanguageList { get; set; }
    }

   

}
