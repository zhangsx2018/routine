using Microsoft.EntityFrameworkCore;
using routine.Api.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace routine.Api.Services
{
    public class Repository<T, E> : IRepository<E>
        where T :DbContext
        where E :class
    {
        private readonly DbContext _dbContext;
        
        public Repository(T t )
        {
            _dbContext = t  ?? throw new ArgumentNullException(nameof(_dbContext));
        }

        public void Add(E t)
        {
            _dbContext.Add(t);
        }

        public void Delete(E t)
        {
            _dbContext.Remove(t);
        }
        public IQueryable<E> GetAsync( PageInfo info,E entity, List<Condition> conditions,out int total)
        {
            var _query = _dbContext.Set<E>() as IQueryable<E>;
            total = 0;
            foreach (PropertyInfo property in typeof(E).GetProperties())
            {
                var obj = property.GetValue(entity);
                if (obj != null)
                {
                    conditions.Add(new Condition { Key = property.Name, QuerySymbol = ConditionSymbolEnum.Equal, Value = obj });
                }
            }

            if ((info.page == 0) && (info.rows == 0))
            {
                _query = QueryableExtensions.QueryConditions(_query, conditions);
            }
            else
            {
                _query = QueryableExtensions.QueryConditions(_query, conditions).Pager(info.page, info.rows, out total);
              
            }
            return _query;
        }

   
        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() >= 0;
        }

        public void Update(E t)
        {
           
        }
    }
}
