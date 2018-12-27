using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Models
{
    [Table("RequestLines")]
    public class RequestLine :  Entity<int>
    {
        [Required]
        public int PreRequestId { get; set; }

        [Required]
        [Display(Name = "SmartCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string SmartCode { get; set; }

        [Required]
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


        [Required]
        public int CreateUserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual User CreateUser { get; set; }

        public virtual User UpdateUser { get; set; }

        public virtual PreRequest TheRequest { get; set; }

        public virtual UnitDefinition UnitDefinition { get; set; }

    }
}
