
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Common
{
    public class WebApiHelper
    {
        static string TokeWebApi = "http://192.168.2.42:7016/api/Service/GetToken";
        public static Tuple<string, string> GetQueryString(Dictionary<string, string> parames)
        {
            if (null == parames || parames.Count < 1)
                return new Tuple<string, string>("", "");
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parames);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();
            StringBuilder query = new StringBuilder("");
            StringBuilder queryStr = new StringBuilder("");
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrWhiteSpace(key)) {
                    query.Append(key).Append(value);
                    queryStr.Append("&").Append(key).Append("=").Append(value);
                }
            }
            var queryStr2 = queryStr.ToString();
            queryStr2=queryStr2.Substring(1, queryStr2.Length - 1);
            return new Tuple<string, string>(query.ToString(), queryStr2);
        }
        public static TokenResultMsg GetSignToken(string staffId)
        {
            Dictionary<string, string> parames = new Dictionary<string, string>();
            parames.Add("staffid", staffId);
            Tuple<string, string> parameters = GetQueryString(parames);
            TokenResultMsg token = WebApiHelper.Get<TokenResultMsg>(TokeWebApi, parameters.Item1, parameters.Item2, staffId, false);
            return token;
        }


        private static string GetSignature(string timeStamp, string nonce, string staffId, string data)
        {
            Token token = null;
            var resultMsg = GetSignToken( staffId);
            if (null != resultMsg)
            {
                if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
                {
                    token = resultMsg.Result;
                }
                else
                {
                    throw new Exception(resultMsg.Data.ToString());
                }
            }
            else {
                throw new Exception("token为空,编号为:"+staffId);
            }

            MD5 hash = MD5.Create();
            string signStr = timeStamp + nonce + staffId + token.SignToken.ToString() + data;
            string sortStr = string.Concat(signStr.OrderBy(c => c));
            var bytes = Encoding.UTF8.GetBytes(sortStr);
            var md5Val = hash.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            foreach (var c in md5Val)
            {
                result.Append(c.ToString("X2"));
            }
            return result.ToString().ToUpper();
        }

        public static T Get<T>(string webApi, string query, string queryStr, string staffId, bool sign = true)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webApi + "?" + queryStr);
            string timeStamp = GetTimeStamp();
            string nonce = GetRandom();
            request.Headers.Add("staffid", staffId);
            request.Headers.Add("timestamp", timeStamp);
            request.Headers.Add("nonce", nonce);
            if (sign)
            {
                request.Headers.Add("signature", GetSignature(timeStamp, nonce, staffId, query));
            }
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Timeout = 9000;
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response =(HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
            string strResult = streamReader.ReadToEnd();
            streamReader.Close();
            streamReceive.Close();
            request.Abort();
            response.Close();
            return JsonConvert.DeserializeObject<T>(strResult);
        }

        public static T Post<T>(string webApi, string data, string staffId)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webApi);
            string timeStamp = GetTimeStamp();
            string nonce = GetRandom();
            request.Headers.Add("staffid", staffId);
            request.Headers.Add("timestamp", timeStamp);
            request.Headers.Add("nonce", nonce);
            request.Headers.Add("signature", GetSignature(timeStamp, nonce, staffId, data));
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "application/json";
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(bytes, 0, bytes.Length);
            request.Timeout = 300000;
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(streamReceive, Encoding.UTF8);
            string strResult = streamReader.ReadToEnd();

            reqStream.Close();
            streamReader.Close();
            streamReceive.Close();
            request.Abort();
            response.Close();
            return JsonConvert.DeserializeObject<T>(strResult);
        }


        private static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.Milliseconds).ToString();
        }

        private static string GetRandom()
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int i = rd.Next(0, int.MaxValue);
            return i.ToString();
        }
    }
}
