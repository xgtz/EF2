using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Server.Extension
{
    public class SignExtension
    {
        public static bool Validate(string timeStamp, string nonce, string staffId, string token, string data, string signature)
        {
            string signStr = timeStamp + nonce + staffId + token + data;
            string sortStr = string.Concat(signStr.OrderBy(c => c));
            var bytes = Encoding.UTF8.GetBytes(sortStr);
            MD5 hash = MD5.Create();
            var md5Val = hash.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            foreach (var c in md5Val) {
                result.Append(c.ToString("X2"));
            }
            return result.ToString().ToUpper().EndsWith(signature);

        }
    }
}