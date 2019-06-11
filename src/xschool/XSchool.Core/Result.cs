using System;

namespace XSchool.Core
{
    public class Result<T> : Result
    {
        public T Data { get; set; }
    }

    public class Result
    {
        public virtual bool Succeed { get; set; }
        public virtual string Code { get; set; }
        public virtual string Message { get; set; }


        public static Result<T> Success<T>(T obj)
        {
            return new Result<T> { Succeed = true, Code = "00", Data = obj, Message = "成功" };
        }

        public static Result Success()
        {
            return new Result { Succeed = true, Code = "00", Message = "成功" };
        }

        public static Result Fail(string message, string code)
        {
            return new Result { Succeed = false, Code = code, Message = message };
        }

        public static Result Fail(string message)
        {
            return new Result { Succeed = false, Code = int.MinValue.ToString(), Message = message };
        }



        public static Result Fail()
        {
            return new Result { Succeed = false, Code = int.MinValue.ToString(), Message = "失败" };
        }

        public static Result Return(Func<bool> func)
        {
            var succeed = func();
            return succeed ? Success() : Fail();
        }

    }
}
