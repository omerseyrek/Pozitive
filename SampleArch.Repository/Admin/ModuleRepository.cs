using SampleArch.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Admin
{

    public class ModuleRepository : GenericRepository<Module>, IModuleRepository
    {
        public ModuleRepository(DbContext context)
            : base(context)
        {
        }


    }
}