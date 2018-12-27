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

namespace SampleArch.Repository.Stock
{

    public class UnitDefinitionRepository : GenericRepository<UnitDefinition>, IUnitDefinitionRepository
    {
        ISessionManager _sessionManeger;
        public UnitDefinitionRepository(DbContext context, ISessionManager sessionManeger)
            : base(context)
        {
            _sessionManeger = sessionManeger;
        }

        public IEnumerable<UnitDefinitionViewModel> ListByFilter(System.Linq.Expressions.Expression<Func<UnitDefinition, bool>> predicate)
        {
            string entityType = typeof(UnitDefinition).ToString();
            string languageCode = _sessionManeger.GetUserProfileLang();

            SampleArchContext ctx = _entities as SampleArchContext;

            IQueryable<UnitDefinition> qBase = null;
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
                        select (new UnitDefinitionViewModel()
                        {
                            Id = r.Id,
                            Code = r.Code,
                            Description = m == null ? "xyz" : m.Value
                        });


            return query.AsEnumerable<UnitDefinitionViewModel>();
        }

    }
}
