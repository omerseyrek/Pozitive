using SampleArch.Model.Core;
using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SampleArch.Model.ViewModels
{
    public class StockViewModel : BaseViewModel
    {

        [Required]
        public int CategoryId { get; set; }


        [StringLength(50)]
        [Display(Name = "SmartCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string SmartCode { get; set; }

        [Display(Name = "CatgoryCode", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string CatgoryCode { get; set; }

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

        [Display(Name = "SupplierName", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string SupplierName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "StockName", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string StockName { get; set; }


        [Required]
        [StringLength(500)]
        [Display(Name = "Definition", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string Definition { get; set; }

        [Display(Name = "UnitName", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public string UnitName { get; set; }

        [Required]
        [Display(Name = "UnitName", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int StockUnitId { get; set; }

        public EnumDefinitions.StockViewMode ViewMode { get; set; }

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

        public virtual List<StockImage> StockImages { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase SmallImage { get; set; }

        public Byte[] SmallImageBinary { get; set; }


        [DataType(DataType.Upload)]
        public HttpPostedFileBase LargeImage { get; set; }

        public Byte[] LargeImageBinary { get; set; }
    }
}
