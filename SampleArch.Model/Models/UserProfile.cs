using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Models
{
    public partial class UserProfile :  Entity<int>
    {
        public string UserId { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
    }
}
