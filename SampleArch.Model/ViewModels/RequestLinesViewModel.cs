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
    public class RequestLineViewModel  : BaseViewModel
    {
        [Required]
        public int PreRequestId { get; set; }


        [Display(Name = "SmartCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string SmartCode { get; set; }


        [Display(Name = "SupplierProductCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string SupplierProductCode { get; set; }

        [Required]
        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }


        [Required]
        [Display(Name = "Quantity", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "StockUnitId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int StockUnitId { get; set; }


        [Display(Name = "StockUnitId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string StockUnitCode { get; set; }

        public virtual PreRequest PreRequest { get; set; }

        public virtual UnitDefinition UnitDefinition { get; set; }
    }
}
