using System.ComponentModel.DataAnnotations;

namespace MCSoft.Infrastructure.Result
{
    public enum ResultCodeEnum
    {
        [Display(Name = "请求(或处理)成功")]
        Success = 200, //请求(或处理)成功

        [Display(Name = "内部请求出错")]
        Error = 500, //内部请求出错

        [Display(Name = "未授权标识")]
        Unauthorized = 401,//未授权标识

        [Display(Name = "请求参数不完整或不正确")]
        ParameterError = 400,//请求参数不完整或不正确

        [Display(Name = "请求TOKEN失效")]
        TokenInvalid = 403,//请求TOKEN失效

        [Display(Name = "HTTP请求类型不合法")]
        HttpMehtodError = 405,//HTTP请求类型不合法

        [Display(Name = "HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,//HTTP请求不合法

        [Display(Name = "该URL已经失效")]
        URLExpireError = 407,//HTTP请求不合法
    }
}
