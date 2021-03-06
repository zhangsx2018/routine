﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace routine.Api.Vo
{
    public class ExpressionParser<T>
    {
        /// <summary>
        /// <see cref="Expression"/>解析器。
        /// </summary>
        /// <typeparam name="T"></typeparam>

        public ParameterExpression Parameter { get; } = Expression.Parameter(typeof(T));
        public Expression<Func<T, bool>> ParserConditions(IEnumerable<Condition> conditions)
        {
            //将条件转化成表达是的Body
            var query = ParseExpressionBody(conditions);
            return Expression.Lambda<Func<T, bool>>(query, Parameter);
        }

        private Expression ParseExpressionBody(IEnumerable<Condition> conditions)
        {
            if (conditions == null || conditions.Count() == 0)
            {
                return Expression.Constant(true, typeof(bool));
            }
            else if (conditions.Count() == 1)
            {
                return ParseCondition(conditions.First());
            }
            else
            {
                Expression left = ParseCondition(conditions.First());
                Expression right = ParseExpressionBody(conditions.Skip(1));
                return Expression.AndAlso(left, right);
            }
        }

        private Expression ParseCondition(Condition condition)
        {
            Expression key = Expression.Property(Parameter, condition.Key);
            //通过Tuple元组，实现Sql参数化。
            Expression value = ToTuple(condition.Value, key.Type);

            switch (condition.QuerySymbol)
            {
                case ConditionSymbolEnum.Lk:
                    return Expression.Call(key, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), value);
                case ConditionSymbolEnum.Equal:
                    return Expression.Equal(key, value);
                case ConditionSymbolEnum.Gt:
                    return Expression.GreaterThan(key, value);
                case ConditionSymbolEnum.Geq:
                    return Expression.GreaterThanOrEqual(key, value);
                case ConditionSymbolEnum.Le:
                    return Expression.LessThan(key, value);
                case ConditionSymbolEnum.Leq:
                    return Expression.LessThanOrEqual(key, value);
                case ConditionSymbolEnum.Neq:
                    return Expression.NotEqual(key, value);
                case ConditionSymbolEnum.In:
                    return ParaseIn(condition);
                case ConditionSymbolEnum.Bt:
                    return ParaseBetween(condition);
                default:
                    throw new NotImplementedException("不支持此操作。");
            }
        }

        private Expression ParaseBetween(Condition conditions)
        {
            Expression key = Expression.Property(Parameter, conditions.Key);
            var valueArr = conditions.Value.ToString().Split(',');
            if (valueArr.Length != 2)
            {
                throw new NotImplementedException("ParaseBetween参数错误");
            }

            Expression expression = Expression.Constant(true, typeof(bool));
            if (double.TryParse(valueArr[0], out double v1)
                && double.TryParse(valueArr[1], out double v2))
            {
                Expression startvalue = ToTuple(v1, typeof(double));
                Expression start = Expression.GreaterThanOrEqual(key, Expression.Convert(startvalue, key.Type));
                Expression endvalue = ToTuple(v2, typeof(double));
                Expression end = Expression.LessThanOrEqual(key, Expression.Convert(endvalue, key.Type));
                return Expression.AndAlso(start, end);
            }
            else if (DateTime.TryParse(valueArr[0], out DateTime v3)
                && DateTime.TryParse(valueArr[1], out DateTime v4))
            {
                Expression startvalue = ToTuple(v3, typeof(DateTime));
                Expression start = Expression.GreaterThanOrEqual(key, Expression.Convert(startvalue, key.Type));
                Expression endvalue = ToTuple(v4, typeof(DateTime));
                Expression end = Expression.LessThanOrEqual(key, Expression.Convert(endvalue, key.Type));
                return Expression.AndAlso(start, end);
            }
            else
            {
                throw new NotImplementedException("ParaseBetween参数错误");
            }
        }

        private Expression ParaseIn(Condition conditions)
        {
            Expression key = Expression.Property(Parameter, conditions.Key);
            //var valueArr = conditions.Value.ToString().Split(',');
            var valueArr = conditions.Value;

            Type type = key.Type;

            Expression expression = Expression.Constant(false, typeof(bool));

            if (type == typeof(string))
            {
                foreach (var itemVal in valueArr as List<string>)
                {
                    expression = ParseValueArr(key, itemVal, expression);
                }
            }
            else if (type.GenericTypeArguments[0] == typeof(int))
            {
                foreach (var itemVal in valueArr as List<int>)
                {
                    expression = ParseValueArr(key, itemVal, expression);
                }
            }
            else if (type.GenericTypeArguments[0] == typeof(long))
            {
                foreach (var itemVal in valueArr as List<long>)
                {
                    expression = ParseValueArr(key, itemVal, expression);
                }
            }
            else
            {
                throw new NotImplementedException("不支持此操作。");
            }
            //foreach (var itemVal in (List<long>)valueArr)
            //{
            //    //Expression value = Expression.Constant(itemVal);
            //    ////Expression value = ToTuple(itemVal, typeof(string));
            //    //Expression right = Expression.Equal(key, Expression.Convert(value, key.Type));
            //    //expression = Expression.Or(expression, right);
            //    expression= parseValueArr(key, itemVal, expression);
            //}
            return expression;
        }

        private Expression ParseValueArr(Expression key, Object itemVal, Expression expression)
        {
            Expression value = Expression.Constant(itemVal);
            //Expression value = ToTuple(itemVal, typeof(string));
            Expression right = Expression.Equal(key, Expression.Convert(value, key.Type));
            return Expression.Or(expression, right);

        }

        private Expression ToTuple(object value, Type type)
        {
            var tuple = Tuple.Create(value);
            return Expression.Convert(
                 Expression.Property(Expression.Constant(tuple), nameof(tuple.Item1))
                 , type);
        }

    }


}
