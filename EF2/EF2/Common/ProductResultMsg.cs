using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Common
{
    public class ProductResultMsg : HttpResponseMsg
    {
        public Product Result
        {
            get
            {
                if (StatusCode == (int)StatusCodeEnum.Success)
                {
                    return JsonConvert.DeserializeObject<Product>(Data.ToString());
                }

                return null;
            }
        }
    }
}
