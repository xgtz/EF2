using EF2.Context;
using EF2.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EF2.Service
{
    public class ProceService
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
                        var baseXml = "<DATA><ITEM ID=\"1\" NAME=\"陈平安\" ISMALE=\"1\" STUDENTNUMBER=\"100\" ></ITEM></DATA>";
                        var c = 0;

                        OracleParameter p1 = new OracleParameter()
                        {
                            ParameterName = "a_BASE_XML",
                            Value = baseXml,
                            OracleDbType = OracleDbType.Varchar2
                        };

                        OracleParameter p2 = new OracleParameter()
                        {
                            ParameterName = "io_cursor",
                            OracleDbType = OracleDbType.RefCursor,
                            Direction = ParameterDirection.InputOutput
                        };
                        var oralceParameters=new OracleParameter[]{ p1, p2};

                        //OracleCommand cmd = new OracleCommand();
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandText = "PK_STUDENT.USP_STUDENT";
                        //cmd.Parameters.AddRange(oralceParameters);
                        //cmd.Connection = new OracleConnection(context.Database.Connection.ConnectionString);
                        //OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                        //DataTable table = new DataTable();
                        //adapter.Fill(table);
                        //foreach (DataRow row in table.Rows)
                        //{
                        //    Console.WriteLine("{0} {1}", row[0], row[1]);
                        //}
                        //cmd.Connection.Close();



                        //DbCommand cmd = context.Database.Connection.CreateCommand();
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.CommandText = "PK_STUDENT.USP_STUDENT";
                        //cmd.Parameters.AddRange(oralceParameters);
                        //cmd.Connection.Open();
                        //using (DbDataReader reader = cmd.ExecuteReader())
                        //{
                        //    while (reader.Read())
                        //    {
                        //        Console.WriteLine("ROWCOUNT:" + reader[0] + ",ERR_MSG:" + reader[1]);     //输出当前行的第一列，第二列数据
                        //    }
                        //}

                        //DbProviderFactory factory = DbProviderFactories.GetFactory(context.Database.Connection);
                        //DbDataAdapter adapter = factory.CreateDataAdapter();
                        //adapter.SelectCommand = cmd;
                        //DataTable table = new DataTable();
                        //adapter.Fill(table);
                        //foreach (DataRow row in table.Rows)
                        //{
                        //    Console.WriteLine("{0} {1}", row[0], row[1]);
                        //}
                        //cmd.Connection.Close();

                        DbRawSqlQuery<Result> query = context.Database.SqlQuery<Result>("begin PK_STUDENT.USP_STUDENT (:a_BASE_XML,:io_cursor); end;", p1, p2);
                        List<Result> resultLst = query.ToList();
                        foreach (var r in resultLst)
                        {
                            Console.WriteLine("ROWCOUNT:{0},ERR_MSG:{1}", r.ROWCOUNT, r.ERR_MSG);
                        }

                        

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
