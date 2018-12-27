using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.CRM
{

    public class StockRepository : GenericRepository<SampleArch.Model.Models.Stock>, IStockRepository
    {
        public StockRepository(DbContext context)
            : base(context)
        {
        }

    }
}
