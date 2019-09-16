using Newtonsoft.Json;
using Server.Extension;
using Server.Model;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Server.Controllers
{
    public class ServiceController : ApiController
    {
        public HttpResponseMessage GetToken(string staffId)
        {
           

            ResultMsg resultMsg = null;
            if (string.IsNullOrEmpty(staffId)) {
                resultMsg = new ResultMsg()
                {
                    StatusCode = (int)StatusCodeEnum.ParameterError,
                    Info = StatusCodeEnum.ParameterError.GetEnumText(),
                    Data = ""
                };
                return HttpResponseExtension.toJson(resultMsg);
            }
            Token token = (Token)HttpRuntime.Cache.Get(staffId);
            if (null == HttpRuntime.Cache.Get(staffId))
            {
                token = new Token() { 
                    StaffId = staffId,
                    SignToken = Guid.NewGuid(),
                    ExpireTime = DateTime.Now.AddDays(1)
                };
                HttpRuntime.Cache.Insert(token.StaffId, token, null, token.ExpireTime, TimeSpan.Zero);
            }
            resultMsg = new ResultMsg()
            {
                StatusCode = (int)StatusCodeEnum.Success,
                Info = StatusCodeEnum.Success.GetEnumText(),
                Data = token
            };
            HttpResponseMessage response= HttpResponseExtension.toJson(resultMsg);
            return response;
        }
    }
}
