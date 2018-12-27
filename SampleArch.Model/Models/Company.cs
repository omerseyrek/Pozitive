﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Models
{
    public class Company : Entity<int>
    {
        public Company()
        {
            Stocks = new HashSet<Stock>();
        }

        [Required]
        [StringLength(100)]
        [Display(Name = "FirmName", ResourceType = typeof(Positive.Model.Languages.CRM))]
        public string FirmName { get; set; }

        
        [StringLength(20)]
        [Display(Name = "TaxNumber", ResourceType = typeof(Positive.Model.Languages.CRM))]
        [RegularExpression(@"\d+", ErrorMessage = null, ErrorMessageResourceName = "NumericError", ErrorMessageResourceType = typeof(Positive.Model.Languages.Common))]
        public string TaxNumber { get; set; }

       
        [StringLength(30)]
        [Display(Name = "TaxOffice", ResourceType = typeof(Positive.Model.Languages.CRM))]
        public string TaxOffice { get; set; }

        [StringLength(50)]
        [Display(Name = "MersisNo", ResourceType = typeof(Positive.Model.Languages.CRM))]
        public string MersisNo { get; set; }

        [StringLength(500)]
        [Display(Name = "Note", ResourceType = typeof(Positive.Model.Languages.CRM))]
        public string Note { get; set; }

        public int? CreateUserId { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? ModifyUserId { get; set; }

        public DateTime? ModifyDate { get; set; }

        public virtual User CreateUser { get; set; }

        public virtual User ModifyUser { get; set; }

        public ICollection<Stock> Stocks { get; set; }

        public ICollection<StockPriceCatalog> StockPriceCatalogs { get; set; }
    }
}
