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
     [Table("UnitDefinitions")]
    public partial class UnitDefinition : Entity<int>
    {
        public UnitDefinition()
        {
            StockUnits = new HashSet<Stock>();
        }


        [Required]
        [StringLength(5)]
        [Display(Name = "Code", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Code { get; set; }

        [NotMapped]
        [MultilanguageFieldAttribute("Code")]
        [Display(Name = "Description", ResourceType = typeof(Positive.Model.Languages.Common))]
        public string Description { get; set; }

        public virtual ICollection<Stock> StockUnits { get; set; }

        public virtual ICollection<RequestLine> RequestLines { get; set; }
    }
}
