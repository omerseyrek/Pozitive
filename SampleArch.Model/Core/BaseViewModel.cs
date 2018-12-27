using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Model.Core
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        public int CreateUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int? UpdateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }


        public virtual User CreateUser { get; set; }

        public virtual User ModifyUser { get; set; }

    }
}
