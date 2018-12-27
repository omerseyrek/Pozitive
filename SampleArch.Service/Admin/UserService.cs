using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Repository;
using SampleArch.Repository.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Admin
{

    public class UserService : EntityService<User>, IUserService
    {

        public UserService(IUnitOfWork unitOfWork, 
            IUserRepository theRepository,
             IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, theRepository, mlRepository, langRepository, sessionManager)
        {
      
        }
    }
}
