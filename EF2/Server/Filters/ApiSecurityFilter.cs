using Server.Model;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Server.Extension;
using Server.Common;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace Server.Filters
{
    public class ApiSecurityFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            ServerLog.Log("新的请求");
            ResultMsg resultMsg = null;
            var request = actionContext.Request;
            string method = request.Method.Method;
            string staffid = string.Empty, timestamp = string.Empty, nonce = string.Empty, signature = string.Empty;
            if (request.Headers.Contains("staffid")) {
                staffid = HttpUtility.UrlDecode(request.Headers.GetValues("staffid").FirstOrDefault());
            }
            if (request.Headers.Contains("timestamp")) {
                timestamp = HttpUtility.UrlDecode(request.Headers.GetValues("timestamp").FirstOrDefault());
            }
            if (request.Headers.Contains("nonce")) {
                nonce = HttpUtility.UrlDecode(request.Headers.GetValues("nonce").FirstOrDefault());
            }
            if (request.Headers.Contains("signature"))
            {
                signature = HttpUtility.UrlDecode(request.Headers.GetValues("signature").FirstOrDefault());
            }

            ServerLog.Log("StaffId:" + staffid);
            ServerLog.Log("TimeStamp:" + timestamp);
            ServerLog.Log("nonce:" + nonce);
          

             if (string.IsNullOrWhiteSpace(staffid) || string.IsNullOrWhiteSpace(timestamp) || string.IsNullOrWhiteSpace(nonce))
                {
                    resultMsg = new ResultMsg()
                    {
                        StatusCode = (int)StatusCodeEnum.ParameterError,
                        Info = StatusCodeEnum.ParameterError.GetEnumText(),
                        Data = ""
                    };
                    actionContext.Response = HttpResponseExtension.toJson(resultMsg);
                    base.OnActionExecuting(actionContext);
                    return;
                }

            if (actionContext.ActionDescriptor.ActionName.Equals("GetToken"))
            {
                base.OnActionExecuting(actionContext);
                return;
            }

            //double ts1 = 0;
            //double ts2 = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            //bool timespanvalidate = double.TryParse(timestamp, out ts1);
            //double ts = ts2 - ts1;
            //bool falg = ts > int.Parse(WebSettingsConfig.UrlExpireTime) * 1000;
            //if (falg || (!timespanvalidate))
            //{
            //    resultMsg = new ResultMsg() { 
            //        StatusCode = (int)StatusCodeEnum.URLExpireError,
            //        Info = StatusCodeEnum.URLExpireError.GetEnumText(),
            //        Data=""
            //    };
            //    actionContext.Response = HttpResponseExtension.toJson(resultMsg);
            //    base.OnActionExecuting(actionContext);
            //    return;
            //}
            Token token = (Token)HttpRuntime.Cache.Get(staffid);
            string signtoken = string.Empty;
            if (null == HttpRuntime.Cache.Get(staffid))
            {
                resultMsg = new ResultMsg()
                {
                    StatusCode = (int)StatusCodeEnum.TokenInvalid,
                    Info = StatusCodeEnum.TokenInvalid.GetEnumText(),
                    Data = ""
                };
                actionContext.Response = HttpResponseExtension.toJson(resultMsg);
                base.OnActionExecuting(actionContext);
                return;
            }
            signtoken = token.SignToken.ToString();
            NameValueCollection form = HttpContext.Current.Request.QueryString;
            string data = string.Empty;
            switch (method)
            { 
                case "POST":
                    Stream stream = HttpContext.Current.Request.InputStream;
                    string responseJson = string.Empty;
                    StreamReader streamReader = new StreamReader(stream);
                    data = streamReader.ReadToEnd();
                    break;
                case "GET":
                    IDictionary<string, string> parameters = new Dictionary<string, string>();
                    for (int f = 0; f < form.Count; f++)
                    {
                        string key = form.Keys[f];
                        parameters.Add(key, form[key]);
                    }
                    IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
                    IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();
                    StringBuilder query = new StringBuilder();
                    while (dem.MoveNext())
                    {
                        string key = dem.Current.Key;
                        string value = dem.Current.Value;
                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            query.Append(key).Append(value);
                        }
                    }
                    data = query.ToString();
                    break;
                default:
                    resultMsg = new ResultMsg() { 
                        StatusCode = (int)StatusCodeEnum.HttpMethodError,
                        Info = StatusCodeEnum.HttpMethodError.GetEnumText(),
                        Data=""
                    };
                    actionContext.Response = HttpResponseExtension.toJson(resultMsg);
                    base.OnActionExecuting(actionContext);
                    return;
                   
            }
            bool result = SignExtension.Validate(timestamp, nonce, staffid, signtoken, data, signature);
            if (!result)
            {
                resultMsg = new ResultMsg() { 
                    StatusCode = (int)StatusCodeEnum.HttpRequestError,
                    Info = StatusCodeEnum.HttpRequestError.GetEnumText(),
                    Data=""
                };
                actionContext.Response = HttpResponseExtension.toJson(resultMsg);
                base.OnActionExecuting(actionContext);
                return;
            }
            base.OnActionExecuting(actionContext);
        }
    }
}