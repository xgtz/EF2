using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Common
{
    public enum StatusCodeEnum
    {
        [Text("请求(处理)成功")]
        Success = 200,

        [Text("内部请求出错")]
        Error = 500,

        [Text("未授权标识")]
        Unauthorized = 401,

        [Text("请求参数不完整或不正确")]
        ParameterError = 400,

        [Text("请求TOKEN失效")]
        TokenInvalid = 403,

        [Text("HTTP请求类型不合法")]
        HttpMethodError = 405,

        [Text("HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,

        [Text("该URL已经失效")]
        URLExpireError = 407
    }
}
