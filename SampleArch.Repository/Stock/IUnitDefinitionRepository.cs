using SampleArch.Model;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Stock
{
    public interface IUnitDefinitionRepository : IGenericRepository<UnitDefinition>
    {
        IEnumerable<UnitDefinitionViewModel> ListByFilter(System.Linq.Expressions.Expression<Func<UnitDefinition, bool>> predicate);

    }
}
