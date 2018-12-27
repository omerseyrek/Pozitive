using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.ViewModels
{
    public class AuthorizeViewModel
    {
        public string ModuleName { get; set; }
        public bool Add    { get; set; }
        public bool Delete { get; set; }
        public bool View   { get; set; }
        public bool Update { get; set; }
    }
}
