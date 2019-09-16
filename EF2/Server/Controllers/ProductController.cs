using Server.Model;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Server.Extension;

namespace Server.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetProduct(string id)
        {
            Product product = new Product()
            {
                Id=1,
                Name="江南红衣",
                Count=1,
                Price=9999
            };
            ResultMsg resultMsg = new ResultMsg() {
                StatusCode = (int)StatusCodeEnum.Success,
                Info = StatusCodeEnum.Success.GetEnumText(),
                Data = product
            };
            return HttpResponseExtension.toJson(resultMsg);
        }

        [HttpPost]    
        public HttpResponseMessage AddProduct([FromBody]Product product)
        {
            ResultMsg msg = new ResultMsg()
            {
                StatusCode = (int)StatusCodeEnum.Success,
                Info = StatusCodeEnum.Success.GetEnumText(),
                Data = product
            };
            return HttpResponseExtension.toJson(msg);
        }
    }
}
