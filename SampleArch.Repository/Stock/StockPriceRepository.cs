using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Stock
{

    public class StockPriceRepository : GenericRepository<StockPrice>, IStockPriceRepository
    {
        public StockPriceRepository(DbContext context)
            : base(context)
        {
        }

    }
}
