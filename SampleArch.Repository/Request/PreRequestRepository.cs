using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Request
{

    public class PreRequestRepository : GenericRepository<PreRequest>, IPreRequestRepository
    {
        public PreRequestRepository(DbContext context)
            : base(context)
        {
        }

    }
}
