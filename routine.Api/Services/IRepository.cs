using routine.Api.Entities;
using routine.Api.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Services
{
  public  interface IRepository<T>
    {
        IQueryable<T> GetAsync(PageInfo info, T entity, List<Condition> conditions, out int total);
   
        void Add(T t);
        void Update(T t);
        void Delete(T t);
     
        Task<int> SaveAsync();
    }
}
