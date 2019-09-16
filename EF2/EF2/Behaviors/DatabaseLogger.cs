using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Behaviors
{
    public class DatabaseLogger : IDbCommandInterceptor
    {
        static readonly ConcurrentDictionary<DbCommand, DateTime> MStartTime = new ConcurrentDictionary<DbCommand, DateTime>();

        public static void OnStart(DbCommand command)
        {
            MStartTime.TryAdd(command, DateTime.Now);
        }

        private static void Log<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            var filePath = @"c:\log\EF\2019.log";
            var logBuilder = new StringBuilder();
            DateTime startTime;
            TimeSpan duration;
            MStartTime.TryRemove(command, out startTime);
            if (startTime != default(DateTime))
            {
                duration = DateTime.Now - startTime;
            }
            else {
                duration = TimeSpan.Zero;
            }
            var parameters = new StringBuilder();
            foreach (DbParameter param in command.Parameters)
            {
                parameters.Append(param.ParameterName + " " + param.DbType + " = " + param.Value);
            }

            //判断语句是否执行时间超过1秒或是否有错
            if (duration.TotalSeconds > 1 || interceptionContext.Exception != null)
            {
                //这里编写记录执行超长时间SQL语句和错误信息的代码
                logBuilder.Append("****************************开始时间：" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + "******************************" + Environment.NewLine);
                logBuilder.Append("执行的脚本:【" + command.CommandText.Replace(Environment.NewLine, "") + "】" + Environment.NewLine);

                if (command.Parameters.Count > 0)
                {
                    logBuilder.Append("执行的参数:【");
                    foreach (DbParameter param in command.Parameters)
                    {
                        logBuilder.Append(string.Format("Name:{0},Type:{1},Value:{2}", param.ParameterName, param.DbType, param.Value));
                    }
                    logBuilder.Append("】" + Environment.NewLine);
                    
                }

                if (null != interceptionContext.Exception)
                {
                    logBuilder.Append("异常信息【" + interceptionContext.Exception.InnerException.Message + "】"+Environment.NewLine);
                }

                logBuilder.Append("****************************结束时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "******************************" + Environment.NewLine + Environment.NewLine);

                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, 20480, true);
                byte[] data = Encoding.UTF8.GetBytes(logBuilder.ToString());
                //异步写文件
                IAsyncResult async = fs.BeginWrite(data, 0, data.Length, asyncResult =>
                {
                    fs.EndWrite(asyncResult);//写文件介绍,输出到text.txt文件中.
                    fs.Close();
                }, null);
            }
            else 
            {
                logBuilder.Append("****************************开始时间：" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + "******************************" + Environment.NewLine);
                logBuilder.Append("执行的脚本:【"+ command.CommandText.Replace(Environment.NewLine,"")+"】"+Environment.NewLine);

                if (command.Parameters.Count > 0)
                {
                    logBuilder.Append("执行的参数:【");
                    foreach (DbParameter param in command.Parameters)
                    {
                        logBuilder.Append(string.Format("Name:{0},Type:{1},Value:{2}",param.ParameterName,param.DbType,param.Value));
                    }
                    logBuilder.Append("】"+Environment.NewLine);
                    
                }
                logBuilder.Append("****************************结束时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "******************************" + Environment.NewLine + Environment.NewLine);
                FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None, 20480, true);
                byte[] data = Encoding.UTF8.GetBytes(logBuilder.ToString());
                //异步写文件
                IAsyncResult async = fs.BeginWrite(data, 0, data.Length, asyncResult =>
                {
                    fs.EndWrite(asyncResult);//写文件介绍,输出到text.txt文件中.
                    fs.Close();
                }, null);
            }

        }

        // 每个执行都有ed(执行完成后的监听)和ing(执行时的监听)
        public void NonQueryExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void NonQueryExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            OnStart(command);
        }

        public void ReaderExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            OnStart(command);
        }

        public void ScalarExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void ScalarExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            OnStart(command);
        }
    }
}
