using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Request
{
    public interface IPreRequestService : IEntityService<PreRequest>
    {
        void CreateWithLines(PreRequestViewModel model);
        void DeleteWithLines(PreRequestViewModel model);
        void UpdateWithLines(PreRequestViewModel model);
    }
}
