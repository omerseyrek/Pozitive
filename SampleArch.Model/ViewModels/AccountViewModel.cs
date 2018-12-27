
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleArch.Model.ViewModels
{

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public bool RememberMe { get; set; }
    }
}
