using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Server.Common
{
    public class ServerLog
    {
        public static void Log(string content)
        {
            var filePath = @"c:\log\EF\2019.log";
            var logBuilder = new StringBuilder();
       
            logBuilder.Append("****************************开始******************************" + Environment.NewLine);
            logBuilder.Append(content+Environment.NewLine);
            logBuilder.Append("****************************截止******************************" + Environment.NewLine);

            FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, 20480, true);
            byte[] data = Encoding.UTF8.GetBytes(logBuilder.ToString());
            fs.Write(data, 0, data.Length);
            fs.Close();
          
        }
    }
}