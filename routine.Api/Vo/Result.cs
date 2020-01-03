using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api
{
    public class Result
    {


        public int code { get; set; }
        public object data { get; set; }
        public string msg { get;set; }


      public   Result(int code, object data)
        {
            this.code = code;
            this.data = data;
        }



        public static Result SUCCESS()
        {
            return new Result(0, (string)null);
        }

        public static Result FAIL()
        {
            return new Result(1, (string)null);
        }

        public string getMsg()
        {
            return this.msg;
        }

        public Result setMsg(string msg)
        {
            this.msg = msg;
            return this;
        }

        public Result setData(object data)
        {
            this.data = data;
            return this;
        }

        public Object getData()
        {
            return this.data;
        }

    }
}
