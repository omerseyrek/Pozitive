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

    public partial class StockPriceCatalogViewModel : BaseViewModel
    {
        public StockPriceCatalogViewModel()
        {

        }


        [Required]
        [Display(Name = "SupplierId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int SupplierId { get; set; }


        [Required]
        [StringLength(20)]
        [Display(Name = "CatalogCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string CatalogCode { get; set; }



        [StringLength(500)]
        [Display(Name = "Definition", ResourceType = typeof(Positive.Model.Languages.Stock))]
        [MultilanguageFieldAttribute("Id")]
        public string Definition { get; set; }


 
        [Display(Name = "SupplierId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string SupplierName { get; set; }


        public virtual Company SupplierCompany { get; set; }

        public virtual IEnumerable<StockPriceViewModel> StockPriceViewModels { get; set; }

        public virtual IEnumerable<int> DestroyedIDs { get; set; }
        
        }

}
