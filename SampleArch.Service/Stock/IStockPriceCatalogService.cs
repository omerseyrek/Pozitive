using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Stock
{
    public interface IStockPriceCatalogService : IEntityService<StockPriceCatalog>
    {
        void CreateWithLines(StockPriceCatalogViewModel model);
        void DeleteWithLines(StockPriceCatalogViewModel model);
        void UpdateWithLines(StockPriceCatalogViewModel model);
    }
}
