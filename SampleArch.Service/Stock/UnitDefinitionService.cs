using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using SampleArch.Repository.Stock;
using SampleArch.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Admin
{

    public class UnitDefinitionService : EntityService<UnitDefinition>, IUnitDefinitionService
    {
        public UnitDefinitionService(IUnitOfWork unitOfWork, 
            IUnitDefinitionRepository unitDefinitionRepository, 
            IMultilanguageRepository mlRepository, 
            IUILanguageRepository langRepository,
            ISessionManager sessionManager
            )
            : base(unitOfWork, unitDefinitionRepository, mlRepository, langRepository, sessionManager)
        {
         
        }

        public IEnumerable<UnitDefinitionViewModel> ListByFilter(System.Linq.Expressions.Expression<Func<UnitDefinition, bool>> predicate)
        {
            return (base.TheRepository as IUnitDefinitionRepository).ListByFilter(predicate);
        }

    
    }
}
