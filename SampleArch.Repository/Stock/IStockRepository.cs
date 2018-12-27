using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleArch.Model.Models;

namespace SampleArch.Repository
{
    public interface IStockRepository : IGenericRepository<SampleArch.Model.Models.Stock>
    {
    }
}
