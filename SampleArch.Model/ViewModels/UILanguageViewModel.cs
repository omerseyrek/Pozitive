using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{
    public class UILanguageViewModel 
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string LangCode { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}
