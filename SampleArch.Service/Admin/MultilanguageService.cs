using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Admin
{
    public class MultilanguageService : EntityService<MultiLanguage>, IMultiLanguageService
    {
        public MultilanguageService(IUnitOfWork unitOfWork,
            IMultilanguageRepository theRepository,
            IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, theRepository, mlRepository, langRepository, sessionManager)
        {
        }

    }
}
