using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Core
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class MultilanguageFieldAttribute : Attribute
    {
        public string TheCodeField = "";

        public MultilanguageFieldAttribute(string theCodeField)
        {
            TheCodeField = theCodeField;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class MultilanguageEntityAttribute : Attribute
    {
        public string TheCodeField = "";

        public MultilanguageEntityAttribute(string theCodeField)
        {
            TheCodeField = theCodeField;
        }
    }
}
