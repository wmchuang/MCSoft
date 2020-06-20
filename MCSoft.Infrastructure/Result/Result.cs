using System;
using System.Collections.Generic;
using System.Text;

namespace MCSoft.Infrastructure.Result
{
    /// <summary>
    /// 服务器请求结果
    /// </summary>
    public interface IResult
    {
        bool status { get; set; }
        string message { get; set; }
    }

    public class ResultBase : IResult
    {
        public ResultBase()
        {
            code = (int)ResultCodeEnum.Success;
            status = true;
            message = "操作成功";
        }

        public static ResultBase Success(string message = "操作成功", int result = (int)ResultCodeEnum.Success)
        {
            return new ResultBase()
            {
                code = result,
                message = message
            };
        }


        public static ResultBase Error(string e = "操作失败")
        {
            return new ResultBase()
            {
                status = false,
                code = (int)ResultCodeEnum.Error,
                message = e
            };
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 状态(是否成功)
        /// </summary>
        public bool status { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }
    }

    public class Result<T> : ResultBase
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }

        public static Result<T> Success(T t, string message = "操作成功", int result = (int)ResultCodeEnum.Success)
        {
            return new Result<T>()
            {
                data = t,
                code = result,
                message = message
            };
        }


        public static new Result<T> Error(string message = "保存失败")
        {
            return new Result<T>()
            {
                status = false,
                code = (int)ResultCodeEnum.Error,
                message = message
            };
        }
    }

    public class ResultList<T> : ResultBase
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public int totalCount { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> list { get; set; }
    }
}
