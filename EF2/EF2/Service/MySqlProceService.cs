using EF2.Context;
using EF2.Model;
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
    public class MySqlProceService
    {
        public static void Test()
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            //    Database.SetInitializer<MySqlContext>(null);
            //    using (var context = new MySqlContext())
            //    {
            //        context.Database.Log = Console.WriteLine;
            //        try
            //        {
            //            //var users = context.Users.Local;
            //            Console.WriteLine(string.Format("用户的数量:{0}", context.Users.Count()));
            //            foreach (User u in context.Users)
            //            {
            //                Console.WriteLine(string.Format("ID:{0},NAME:{1}", u.ID, u.Name));
            //            }

            //            scope.Complete();
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine(e.Message);
            //        }
            //    }
            //}


            using (TransactionScope scope = new TransactionScope())
            {
                Database.SetInitializer<MySqlContext>(null);
                using (var context = new MySqlContext())
                {
                    try
                    {
                        context.Database.Log = Console.WriteLine;

                        //var sGuid1 = Guid.NewGuid().ToString();
                        //var sGuid2 = Guid.NewGuid().ToString();
                        //var tGuid1 = Guid.NewGuid().ToString();
                        //var tGuid2 = Guid.NewGuid().ToString();

                        //Teacher t1 = new Teacher()
                        //{
                        //    ID = tGuid1,
                        //    Name = "张巨鹿"
                        //};

                        //context.Entry(t1).State = EntityState.Added;
                        //int c= context.SaveChanges();
                        //Console.WriteLine(string.Format("保存Teach影响的行数:{0}", c));


                        var baseXml = "<DATA><ITEM ID=\"1\" NAME=\"陈平安\" ISMALE=\"1\" STUDENTNUMBER=\"100\" ></ITEM></DATA>";
                        var c = 0;

                        SqlParameter p1 = new SqlParameter()
                        {
                            ParameterName = "BASIC_XML",
                            Value = baseXml,
                            SqlDbType = SqlDbType.Xml
                        };

                        var query = context.Database.SqlQuery<Result>("exec USP_STUDENT @BASIC_XML", p1);
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
