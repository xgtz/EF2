using EF2.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Service
{
    public class MsSqlInsertService
    {
        public static void Test()
        {
            Database.SetInitializer<MySqlContext>(null);
            using (var db = new MySqlContext())
            {
                db.Database.Log = Console.WriteLine;

                var sGuid1 = Guid.NewGuid().ToString();
                var sGuid2 = Guid.NewGuid().ToString();
                var tGuid1 = Guid.NewGuid().ToString();
                var tGuid2 = Guid.NewGuid().ToString();

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
            



                t1.Students.Add(s1);
                t1.Students.Add(s2);
                t2.Students.Add(s1);
                t2.Students.Add(s2);
                db.Entry(s1).State = EntityState.Added;
                db.Entry(s2).State = EntityState.Added;
                db.Entry(t1).State = EntityState.Added;
                db.Entry(t2).State = EntityState.Added;


                try
                {
                    int c = db.SaveChanges();
                    Console.WriteLine(string.Format("SaveChanges Count:{0}", c));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    Console.WriteLine("----------------------------学生信息-------------------------------------");

                    var studentLst = db.Students.ToList();
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

                try
                {
                    Console.WriteLine("----------------------------教师信息-------------------------------------");
                    var teachLst = db.Teachers.ToList();
                    Console.WriteLine(string.Format("老师的数量:{0}", teachLst.Count()));
                    foreach (var t in teachLst)
                    {
                        Console.WriteLine(string.Format("ID:{0},Name:{1}", t.ID, t.Name));
                        foreach (Student sd in t.Students)
                        {
                            Console.WriteLine(string.Format("老师对应的学生:(-- ID:{0},姓名:{1}--)", sd.ID, sd.Name));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }


        }
    }
}
