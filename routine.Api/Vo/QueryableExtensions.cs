using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace routine.Api.Vo
{
    public static partial class QueryableExtensions
    {
        /// <summary>
        /// 添加查询条件。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static IQueryable<T> QueryConditions<T>(this IQueryable<T> query, IEnumerable<Condition> conditions)
        {
            var parser = new ExpressionParser<T>();
            var filter = parser.ParserConditions(conditions);
            query = query.Where(filter);
            foreach (var orderinfo in conditions.Where(c => c.Order != OrderEnum.None))
            {
                var t = typeof(T);
                var propertyInfo = t.GetProperty(orderinfo.Key);
                var parameter = Expression.Parameter(t);
                Expression propertySelector = Expression.Property(parameter, propertyInfo);

                var orderby = Expression.Lambda<Func<T, object>>(Expression.Convert(propertySelector, typeof(object)), parameter);
                if (orderinfo.Order == OrderEnum.Desc)
                    query = query.OrderByDescending(orderby);
                else
                    query = query.OrderBy(orderby);

            }
            return query;

        }
        /// <summary>
        /// 分页。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <param name="itemCount"></param>
        /// <returns></returns>
        public static IQueryable<T> Pager<T>(this IQueryable<T> query, int pageindex, int pagesize, out int itemCount)
        {
            itemCount = query.Count();
            return query.Skip((pageindex - 1) * pagesize).Take(pagesize);
        }

    }
}
