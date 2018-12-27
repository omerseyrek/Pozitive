using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Models
{
    [Table("Stocks")]
    public partial class Stock : Entity<int>
    {
        public Stock()
        {

        }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "SmartCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string SmartCode { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "CodeIndex", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string CodeIndex { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "SupplierProductCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string SupplierProductCode { get; set; }

        [Required]
        [Display(Name = "SupplierId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "StockName", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string StockName { get; set; }


        [Required]
        [StringLength(500)]
        [Display(Name = "Definition", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string Definition { get; set; }


        [Required]
        [Display(Name = "StockUnitId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int StockUnitId { get; set; }

        [Required]
        public int CreateUserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual User CreateUser { get; set; }

        public virtual User UpdateUser { get; set; }

        public virtual Company SupplierCompany { get; set; }

        public virtual Category Category { get; set; }

        public virtual UnitDefinition UnitDefinition { get; set; }

        public virtual ICollection<StockPrice> Prices { get; set; }

        public virtual ICollection<StockImage> StockImages { get; set; }
    }
}
