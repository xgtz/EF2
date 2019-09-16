using EF2.Common;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Test
{
    [TestFixture]
    class ServerApiTest
    {
        string GetWebApiUrl = "",ModuleId="",PostWebApiUrl="";

        [SetUp]
        public void SetUp()
        {
            GetWebApiUrl = "http://192.168.2.42:7016/api/Product/GetProduct";
            PostWebApiUrl = "http://192.168.2.42:7016/api/Product/AddProduct";
            ModuleId = "A09";
        }

         [Test]
        public void GetToken()
        {
            TokenResultMsg result = WebApiHelper.GetSignToken(ModuleId);
            Console.WriteLine(result.Data.ToString());
        }


        [Test]
        public void QueryProduct()
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("id", "1");
            parames.Add("name", "wahaha");
            Tuple<string, string> parameters = WebApiHelper.GetQueryString(parames);
            var product = WebApiHelper.Get<ProductResultMsg>(GetWebApiUrl, parameters.Item1, parameters.Item2, ModuleId);
            Console.WriteLine(product.Data.ToString());
            Console.WriteLine (product.Result.Id.ToString()+":"+product.Result.Name+":"+product.Result.Price.ToString());
           
        }

         [Test]
        public void PostProduct()
        {
            Product product = new Product() { Id = 1, Name = "安慕希", Count = 10, Price = 58.8 };
            var result = WebApiHelper.Post<ProductResultMsg>(PostWebApiUrl, JsonConvert.SerializeObject(product), ModuleId);
            Console.WriteLine(result.Data.ToString());
        }
    }
}
