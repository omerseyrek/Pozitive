using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Models
{
    [Table("StockImage")]
    public partial class StockImage : Entity<int>
    {
        public StockImage()
        {

        }

        [Required]
        [Display(Name = "StockId", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public int StockId { get; set; }

        [Required]
        [Display(Name = "StockImageLarge", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public byte[] StockImageLarge { get; set; }

        [Required]
        [Display(Name = "StockImageSmall", ResourceType = typeof(Positive.Model.Languages.Stock))]
        public byte[] StockImageSmall { get; set; } 

        [Required]
        public int CreateUserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public int? UpdateUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        public virtual User CreateUser { get; set; }

        public virtual User UpdateUser { get; set; }

        [Required]
        public Stock Stock { get; set; }
    }
}
