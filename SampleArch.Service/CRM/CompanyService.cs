using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using SampleArch.Repository.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.CRM
{

    public class CompanyService : EntityService<Company>, ICompanyService
    {
        public CompanyService(IUnitOfWork unitOfWork,
            ICompanyRepository theRepository,
            IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, theRepository, mlRepository, langRepository, sessionManager)
        {
        }
    }
}
