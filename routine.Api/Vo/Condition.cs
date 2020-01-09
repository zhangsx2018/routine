using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Vo
{
    public class Condition
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConditionSymbolEnum QuerySymbol { get; set; }

        public OrderEnum Order { get; set; }

     

    }
}
