using SampleArch.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{
    public class MenusViewModel : BaseViewModel, IDisposable
    {
        #region Properties

        [Display(Name = "ParentId", ResourceType = typeof(Positive.Model.Languages.Common))]
        public int? ModuleId { get; set; }

        [Display(Name = "Module", ResourceType = typeof(Positive.Model.Languages.Admin))]
        public int? ParentId { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string DescriptionLowered { get; set; }

        [Display(Name = "IsActive", ResourceType = typeof(Positive.Model.Languages.Common))]
        public bool IsActive { get; set; }

        public bool hasChildren { get; set; }

        public string Url { get; set; }

        public int CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        public int UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public MenusViewModel Parent { get; set; }

        public List<MenusViewModel> SubMenus { get; set; }
        #endregion

        public void Dispose()
        {
        }
    }
}
