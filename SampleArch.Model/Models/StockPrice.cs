using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Models
{
    [Table("StockPrice")]
    public partial class StockPrice : Entity<int>
    {
        public StockPrice()
        {
        }

        [Required]
        [Display(Name = "CatalogId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int CatalogId { get; set; }


        [Required]
        [Display(Name = "StockId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int StockId { get; set; }


        [Required]
        [Display(Name = "Price", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int Price { get; set; }

        [Required]
        public int CreateUserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual User CreateUser { get; set; }

        public virtual User UpdateUser { get; set; }

        public virtual StockPriceCatalog Catalog { get; set; }

        public virtual Stock StockItem { get; set; }
    }
}
