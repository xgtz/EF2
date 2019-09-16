using EF2.Context;
using EF2.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace EF2.Service
{
    public class ProceCmdService
    {
        public static void Test()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Database.SetInitializer<TestContext>(null);
                Database.SetInitializer<TeacherContext>(null);
                using (var context = new TestContext())
                {
                    try
                    {
                        context.Database.Log = Console.WriteLine;
                        var sGuid1 = Guid.NewGuid().ToString();
                        var baseXml = "<DATA><ITEM ID=\"1\" NAME=\"老高\" ></ITEM></DATA>";

                        OracleParameter p1 = new OracleParameter()
                        {
                            ParameterName = "a_BASE_XML",
                            Value = baseXml,
                            OracleDbType = OracleDbType.Clob
                        };

                        OracleParameter p2 = new OracleParameter()
                        {
                            ParameterName = "o_COUNT",
                            OracleDbType = OracleDbType.Int32,
                            IsNullable=true,
                            Direction = ParameterDirection.InputOutput
                        };

                        OracleParameter p3 = new OracleParameter()
                        {
                            ParameterName = "o_MSG",
                            OracleDbType = OracleDbType.Varchar2,
                            Size = 4000,
                            IsNullable=true,
                            Direction = ParameterDirection.Output
                        };
                        OracleParameter[] parameters = new OracleParameter[] { p1,p2,p3 };
                        int c = context.Database.ExecuteSqlCommand("begin PK_TEACHER.USP_TEACHER (:a_BASE_XML,:o_COUNT,:o_MSG); end;", parameters);
                        Console.WriteLine(string.Format("COUNT:{0},MSG:{1}", parameters[1].Value, parameters[2].Value));
                        //Console.WriteLine(string.Format("COUNT:{0}", parameters[1].Value));

                        scope.Complete();



                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
