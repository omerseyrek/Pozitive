﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SampleArch.Model;
namespace SampleArch.Repository
{
    public interface IGenericRepository<T> 
        where T : BaseEntity
    {

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithInclude(string includings);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindByWithInclude(string includings, Expression<Func<T, bool>> predicate);


        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
        T Get(object v);
        T GetWithInclude(object v, string includings);
        T GetBy(Expression<Func<T, bool>> predicate);
    }
}
