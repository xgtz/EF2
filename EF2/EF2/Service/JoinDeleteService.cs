using EF2.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Service
{
    public class JoinDeleteService
    {
        public static void Test()
        {
            Database.SetInitializer<TestContext>(null);
            using (var db = new TestContext())
            {
                db.Database.Log = Console.WriteLine;

                try
                {
                    var gdS1 = new Guid("8d24144e-818f-4fc6-8d0f-2c6070031759").ToString();
                    var s1 = db.Students.Find(gdS1);
                    Console.WriteLine(string.Format("ID:{0},NAME:{1},SEX:{2}",s1.ID,s1.Name,s1.IsMale));
                    Console.WriteLine(s1.Teachers.Count());
                    //foreach (var t in s1.Teachers)
                    //{
                    //    Console.WriteLine(string.Format("医生信息(--ID:{0},NAME:{1}--)", t.ID, t.Name));
                    //}


                    db.Entry(s1).State = EntityState.Deleted;
                    
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
