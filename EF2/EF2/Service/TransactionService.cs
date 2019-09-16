using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EF2.Context;
using System.Data.SqlClient;
using EF2.Model;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;
namespace EF2.Service
{
    public class TransactionService
    {

        public static void Test()
        {
            Database.SetInitializer<TestContext>(null);
            Database.SetInitializer<TeacherContext>(null);
            using (var context = new TestContext())
            {
                context.Database.Log = Console.WriteLine;

                var sGuid1 = Guid.NewGuid().ToString();
                var sGuid2 = Guid.NewGuid().ToString();
                var tGuid1 = Guid.NewGuid().ToString();
                var tGuid2 = Guid.NewGuid().ToString();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Student s1 = new Student()
                        {
                            ID = sGuid1,
                            Name = "张萨姆",
                            StudentNumber = 1,
                            IsMale = true
                        };

                        Student s2 = new Student()
                        {
                            ID = sGuid2,
                            Name = "张市长",
                            StudentNumber = 2,
                            IsMale = false
                        };

                        Teacher t1 = new Teacher()
                        {
                            ID = tGuid1,
                            Name = "张巨鹿"
                        };


                        Teacher t2 = new Teacher()
                        {
                            ID = tGuid2,
                            Name = "老黄"
                        };

                        t1.Students.Add(s1);
                        t1.Students.Add(s2);
                        t2.Students.Add(s1);
                        t2.Students.Add(s2);
                        context.Entry(s1).State = EntityState.Added;
                        context.Entry(s2).State = EntityState.Added;
                        context.Entry(t1).State = EntityState.Added;
                        context.Entry(t2).State = EntityState.Added;

                        TeacherContext thContext = new TeacherContext(
                            context.Database.Connection,
                            contextOwnsConnection: false);
                        thContext.Database.UseTransaction(transaction.UnderlyingTransaction);

                        int c = context.SaveChanges();
                        Console.WriteLine("SaveChanges影响的行数:{0}", c);

                        //c = thContext.Database.ExecuteSqlCommand(" UPDATE STUDENT SET NAME=:NAME WHERE ID=:ID", new OracleParameter[]{
                        //    new OracleParameter("@NAME","东源"),
                        //    new OracleParameter("@ID",sGuid1),
                        //});
                        //Console.WriteLine("ExecuteSqlCommand影响的行数:{0}", c);

                        t1.Name = "落月";
                        thContext.Entry(t1).State = EntityState.Modified;
                        c=thContext.SaveChanges();
                        Console.WriteLine("thContext的SaveChanges影响的行数:{0}", c);

                        transaction.Commit();

                        try
                        {
                            Console.WriteLine("----------------------------学生信息-------------------------------------");

                            context.Entry(s1).Reload();
                            var studentLst = context.Students.Local;
                            Console.WriteLine(string.Format("学生的数量:{0}", studentLst.Count));
                            foreach (var s in studentLst)
                            {
                                Console.WriteLine(string.Format("ID:{0},Name:{1},Sex:{2}", s.ID, s.Name, s.IsMale));
                                foreach (Teacher tc in s.Teachers)
                                {
                                    Console.WriteLine(string.Format("学生对应的老师( -- ID:{0},姓名:{1} -- )", tc.ID, tc.Name));
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        transaction.Rollback();
                    }
                    
                    
                }
            }
        }
    }
}
