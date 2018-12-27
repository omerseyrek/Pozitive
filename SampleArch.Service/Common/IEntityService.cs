using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleArch.Model;
using System.Linq.Expressions;
using SampleArch.Service.Core;

namespace SampleArch.Service
{
    public interface IEntityService<T> : IService
     where T : BaseEntity
    {
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);

        T Get(object val);
        T GetWithIncludings(object val, string includings);

        IEnumerable<T> GetAll();   
        IEnumerable<T> GetByFilter(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetByFilterWithInclude(string includings, Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAllWithIncludings(string includings);
        IEnumerable<ValidationResult> GetBussinesValidations(BaseEntity model);

    }
}
