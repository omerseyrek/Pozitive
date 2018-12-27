using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Admin
{
    public class MultilanguageRepository : GenericRepository<MultiLanguage>, IMultilanguageRepository
    {
        public MultilanguageRepository(DbContext context)
            : base(context)
        {
        }

    }

}
