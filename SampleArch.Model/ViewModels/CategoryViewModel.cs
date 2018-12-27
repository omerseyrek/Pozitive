using SampleArch.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        public int? ParentId { get; set; }

        public CategoryViewModel ParentCategory { get; set; }

        public List<CategoryViewModel> SubCategories { get; set; }

        [Required]
        [Display(Name = "Key", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string Key { get; set; }

        [Display(Name = "CompleteKey", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string CompleteKey { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Name", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string Name { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }
    }


    public class CategoryViewModelForSearch
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string CompleteKey { get; set; }

        public string Name { get; set; }
    }
}
