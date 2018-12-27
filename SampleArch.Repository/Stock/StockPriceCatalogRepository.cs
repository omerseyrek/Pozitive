using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Stock
{

    public class StockPriceCatalogRepository : GenericRepository<StockPriceCatalog>, IStockPriceCatalogRepository
    {
        public StockPriceCatalogRepository(DbContext context)
            : base(context)
        {
        }

    }
}
