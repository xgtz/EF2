using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EF2.Service
{
    public class MySqlProceCmdService
    {
        public static void Test()
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Database.SetInitializer<MySqlContext>(null);
                using (var context = new MySqlContext())
                {
                    try
                    {
                        context.Database.Log = Console.WriteLine;
                        var baseXml = "<DATA><ITEM ID=\"1\" NAME=\"老黄\"  ></ITEM></DATA>";
                        var c = 0;

                        SqlParameter p1 = new SqlParameter()
                        {
                            ParameterName = "BASIC_XML",
                            Value = baseXml,
                            SqlDbType = SqlDbType.Xml
                        };

                        SqlParameter p2 = new SqlParameter()
                        {
                            ParameterName = "COUNT",
                            SqlDbType = SqlDbType.Int,
                            Direction = ParameterDirection.Output
                        };

                        SqlParameter p3 = new SqlParameter()
                        {
                            ParameterName = "MSG",
                            SqlDbType = SqlDbType.NVarChar,
                            Size = 4000,
                            Direction = ParameterDirection.Output
                        };
                        SqlParameter[] parameters = new SqlParameter[]{ p1,p2,p3};
                        c = context.Database.ExecuteSqlCommand("exec USP_TEACHER @BASIC_XML, @COUNT OUT,@MSG OUT", parameters);
                        Console.WriteLine( string.Format("ExecuteSqlCommand结果:{0}",c));
                        Console.WriteLine(string.Format("Parameter[1]结果:{0}", parameters[1].Value));
                        Console.WriteLine(string.Format("Parameter[2]结果:{0}", parameters[2].Value));

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
