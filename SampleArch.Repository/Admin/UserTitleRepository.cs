using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.Models;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Admin
{

    public class UserTitleRepository : GenericRepository<UserTitle>, IUserTitleRepository
    {
        ISessionManager _sessionManeger;
        public UserTitleRepository(DbContext context, ISessionManager sessionManeger)
            : base(context)
        {
            _sessionManeger = sessionManeger;
        }


        public new IEnumerable<UserTitleViewModel> ListByFilter(System.Linq.Expressions.Expression<Func<UserTitle, bool>> predicate)
        {
            string entityType = typeof(UserTitle).ToString();
            string languageCode = _sessionManeger.GetUserProfileLang();

            SampleArchContext ctx = _entities as SampleArchContext;

            IQueryable<UserTitle> qBase = null;
            if (predicate == null)
            {
                qBase = _dbset;
            }
            else
            {
                qBase = _dbset.Where(predicate);
            }

            var query = from r in qBase
                        join m in ctx.MultiLanguage on new { code = r.Code, Lang = languageCode, ent = entityType } equals new { code = m.Code, Lang = m.LanguageCode, ent = m.EntityType } into ml
                        from m in ml.DefaultIfEmpty(null)
                        select (new UserTitleViewModel()
                        {
                            Id = r.Id,
                            Code = r.Code,
                            Description = m == null ? "" : m.Value
                        });


            return query.AsEnumerable<UserTitleViewModel>();
        }

    
    }
}
