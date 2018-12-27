using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Models
{
    public class MultiLanguage : Entity<int>
    {
        public string EntityType { get; set; }

        public string LanguageCode { get; set; }

        public String Code { get; set; }

        public string Value { get; set; }
    }
}
