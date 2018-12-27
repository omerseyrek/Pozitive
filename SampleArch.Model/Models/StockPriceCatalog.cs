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


    [Table("StockPriceCatalog")]
    public partial class StockPriceCatalog : Entity<int>
    {
        public StockPriceCatalog()
        {

        }

        [Required]
        [Display(Name = "SupplierId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int SupplierId { get; set; }


        [Required]
        [StringLength(20)]
        [Display(Name = "CatalogCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string CatalogCode { get; set; }


        [NotMapped]
        [StringLength(500)]
        [Display(Name = "Definition", ResourceType = typeof(Positive.Model.Languages.Stock))]
        [MultilanguageFieldAttribute("Id")]
        public string Definition { get; set; }

        [Required]
        public int CreateUserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual User CreateUser { get; set; }

        public virtual User UpdateUser { get; set; }

        public virtual Company SupplierCompany { get; set; }

        public virtual ICollection<StockPrice> Prices { get; set; }
    }
}
