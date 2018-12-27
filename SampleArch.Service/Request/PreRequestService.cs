using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
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

    public class PreRequestService : EntityService<PreRequest>, IPreRequestService
    {
        IUnitOfWork _unitOfWork;
        IPreRequestRepository _theRepository;
        IRequestLinesRepository _requestLineRepository;

        public PreRequestService(IUnitOfWork unitOfWork,
            IPreRequestRepository theRepository,
              IRequestLinesRepository requestLineRepository,
            IMultilanguageRepository mlRepository,
            IUILanguageRepository langRepository,
            ISessionManager sessionManager)
            : base(unitOfWork, theRepository, mlRepository, langRepository, sessionManager)
        {
            _unitOfWork = unitOfWork;
            _theRepository = theRepository;
            _requestLineRepository = requestLineRepository;
        }




        public void CreateWithLines(PreRequestViewModel model)
        {
            UserProfileViewModel user = SessionManager.GetUserProfile();

            PreRequest data = new PreRequest();

            if (model.Id > 0)
            {
                data = TheRepository.Get(model.Id);
            }

            PocoHelper.UpdatePocoMapper<PreRequestViewModel, PreRequest>(model, data);

            PocoHelper.SetTractionFieldsOfEntitiy(data, Int32.Parse(user.UserId), DateTime.Now);

            data.Items = new List<RequestLine>();

            foreach (RequestLineViewModel item in model.RequestLines)
            {
                RequestLine line = new RequestLine();

                PocoHelper.UpdatePocoMapper<RequestLineViewModel, RequestLine>(item, line);

                PocoHelper.SetTractionFieldsOfEntitiy(line, Int32.Parse(user.UserId), DateTime.Now);

                data.Items.Add(line);
            }

            TheRepository.Add(data);

            UnitOfWork.Commit();

            if (HasEntityMultilanguageField())
            {
                base.CreateMultilanguageDefinitions(data);
                UnitOfWork.Commit();
            }
        }

        public void DeleteWithLines(PreRequestViewModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateWithLines(PreRequestViewModel model)
        {
            UserProfileViewModel user = SessionManager.GetUserProfile();

            PreRequest data = new PreRequest();

            if (model.Id > 0)
            {
                data = TheRepository.GetWithInclude(model.Id, "Items");
            }

            PocoHelper.UpdatePocoMapper<PreRequestViewModel, PreRequest>(model, data);

            PocoHelper.SetTractionFieldsOfEntitiy(data, Int32.Parse(user.UserId), DateTime.Now);

            if (data.Items == null)
            {
                data.Items = new List<RequestLine>();
            }

            foreach (RequestLineViewModel item in model.RequestLines)
            {
                RequestLine line = new RequestLine();

                if (item.Id == 0)
                {
                    PocoHelper.UpdatePocoMapper<RequestLineViewModel, RequestLine>(item, line);
                    PocoHelper.SetTractionFieldsOfEntitiy(line, Int32.Parse(user.UserId), DateTime.Now);
                    line.PreRequestId = data.Id;
                    data.Items.Add(line);
                }
                else
                {
                    line = data.Items.FirstOrDefault(p => p.Id == item.Id);
                    PocoHelper.UpdatePocoMapper<RequestLineViewModel, RequestLine>(item, line);
                    PocoHelper.SetTractionFieldsOfEntitiy(line, Int32.Parse(user.UserId), DateTime.Now);
                    line.PreRequestId = data.Id;
                }
            }

            if (model.DestroyedIDs != null)
            {
                foreach (int toDestroyID in model.DestroyedIDs)
                {
                    RequestLine line = new RequestLine();
                    if (toDestroyID > 0)
                    {
                        line = data.Items.FirstOrDefault(p => p.Id == toDestroyID);

                        _requestLineRepository.Delete(line);
                    }

                }
            }

            TheRepository.Edit(data);

            UnitOfWork.Commit();

            if (HasEntityMultilanguageField())
            {
                base.CreateMultilanguageDefinitions(data);
                UnitOfWork.Commit();
            }
        }
    }
}
