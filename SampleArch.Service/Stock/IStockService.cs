using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Stock
{
    public interface IStockService : IEntityService<SampleArch.Model.Models.Stock>
    {
    }
}
