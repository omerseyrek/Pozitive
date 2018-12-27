using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Admin
{
    public interface IUserTitleRepository : IGenericRepository<UserTitle>
    {
        IEnumerable<UserTitleViewModel> ListByFilter(System.Linq.Expressions.Expression<Func<UserTitle, bool>> predicate);
    }
}
