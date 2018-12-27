using SampleArch.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{

    public class ModulesViewModel : BaseViewModel
    {

        #region Properties
        [Display(Name = "Code", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public string Code { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool IsActive { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        #endregion

    }
}
