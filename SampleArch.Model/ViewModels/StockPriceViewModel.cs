using SampleArch.Model.Core;
using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{
    public partial class StockPriceViewModel : BaseViewModel
    {
        public StockPriceViewModel()
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


        public virtual StockPriceCatalog Catalog { get; set; }

        [Display(Name = "SupplierId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int SupplierId { get; set; }


        [Display(Name = "SmartCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string SmartCode { get; set; }

        [StringLength(50)]
        [Display(Name = "SupplierProductCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string SupplierProductCode { get; set; }

        [StringLength(100)]
        [Display(Name = "StockName", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string StockName { get; set; }
    }
}
