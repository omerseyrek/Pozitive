using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleArch.Model
{
    [Table("Category")]
    public partial class Category : Entity<int>
    {
        public Category()
        {
            SubCategory = new List<Category>();
            Stocks = new List<Stock>();
        }

        [Display(Name = "ParentId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int? ParentId { get; set; }

        [Display(Name = "Key", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string Key { get; set; }

        [Display(Name = "CompleteKey", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string CompleteKey { get; set; }

        [StringLength(50)]
        [Display(Name = "Name", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }

        public virtual IList<Category> SubCategory { get; set; }

        public virtual Category Parent { get; set; }

        public virtual IList<Stock> Stocks { get; set; }
    }
}
