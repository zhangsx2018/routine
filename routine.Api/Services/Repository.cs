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
        where T : DbContext
        where E : class
    {
        private readonly DbContext _dbContext;

        public Repository(T t)
        {
            _dbContext = t ?? throw new ArgumentNullException(nameof(_dbContext));
        }

        public void Add(E t)
        {
            _dbContext.Set<E>().Add(t);
        }

        public void Delete(E t)
        {
            _dbContext.Set<E>().Remove(t);
        }
        public IQueryable<E> GetAsync(PageInfo info, E entity, List<Condition> conditions, out int total)
        {
            var _query = _dbContext.Set<E>() as IQueryable<E>;
            total = 0;
            foreach (PropertyInfo property in typeof(E).GetProperties())
            {
                var value = property.GetValue(entity);
                if (value != null)
                {
                    if (property.Name.IndexOf("_") != -1)
                    {
                        //属性中条件查询,提取查询方式，和查询字段
                        var prop = property.Name.Split('_');
                        var condition = new Condition() { Value = value, Key = prop[1] };
                        switch (prop[0])
                        {
                            case "Lk":
                                condition.QuerySymbol = ConditionSymbolEnum.Lk;break;
                            case "Neq":
                                condition.QuerySymbol = ConditionSymbolEnum.Neq; break;
                            case "Gt":
                                condition.QuerySymbol = ConditionSymbolEnum.Gt; break;
                            case "Geq":
                                condition.QuerySymbol = ConditionSymbolEnum.Geq; break;
                            case "Le":
                                condition.QuerySymbol = ConditionSymbolEnum.Le; break;
                            case "Leq":
                                condition.QuerySymbol = ConditionSymbolEnum.Leq; break;
                            case "In":
                                condition.QuerySymbol = ConditionSymbolEnum.In; break;
                            case "Bt":
                                condition.QuerySymbol = ConditionSymbolEnum.Bt; break;
                            default: throw new NotImplementedException("不支持此操作。");
                        }
                        conditions.Add(condition);
                    }
                    else
                    {
                        conditions.Add(new Condition { Key = property.Name, QuerySymbol = ConditionSymbolEnum.Equal, Value = value });
                    }



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


        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Update(E t)
        {

        }
    }
}
