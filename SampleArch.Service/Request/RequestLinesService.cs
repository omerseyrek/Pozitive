using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using SampleArch.Repository.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Request
{

    public class RequestLineService : EntityService<RequestLine>, IRequestLineService
    {
        IUnitOfWork _unitOfWork;
        IRequestLinesRepository _theRepository;

        public RequestLineService(IUnitOfWork unitOfWork, 
            IRequestLinesRepository theRepository,
             IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, theRepository, mlRepository, langRepository, sessionManager)
        {
            _unitOfWork = unitOfWork;
            _theRepository = theRepository;
        }
    }
}
