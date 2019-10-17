using EF2.Context;
using EF2.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EF2.Service
{
    public class BFDbContextService
    {
        public static void Test()
        {
            using (var scope = new TransactionScope())
            {
                Database.SetInitializer<BFDbContext>(null);
                using (var context = new BFDbContext())
                {
                    context.Database.Log = Console.WriteLine;
                    try
                    {

                        Nationality cn = new Nationality() { Id=1, Name = "中国" };
                        Nationality us = new Nationality() { Id=2, Name = "美国" };
 
                        //注意这里要给Person类的导航属性赋值,NationalityId属性不用赋值
                        Person cnP = new Person() { Id=1, Name = "徐齐", Age = 86, Nationality = cn };
                        Person usP = new Person() { Id=2, Name = "王穗", Age = 86, Nationality = us };

                        context.Entry(cnP).State = EntityState.Added;
                        context.Entry(usP).State = EntityState.Added;
                        context.SaveChanges();

                        Console.WriteLine(string.Format("人的数量:{0}", context.Person.Count()));
                        var personLst = context.Person.Local;
                        foreach (var p in personLst) {
                            Console.WriteLine(string.Format("ID:{0},Name:{1},Nationality:{2}", p.Id, p.Name, p.Nationality.Name));
                        }
                        Console.WriteLine(string.Format("国家的数量:{0}", context.Nationality.Count()));
                        var nationLst = context.Nationality.Local;
                        foreach (var n in nationLst) {
                            foreach (var p in n.Persons)
                            {
                                Console.WriteLine(string.Format("ID：{0},Name:{1},Person:{2}", n.Id, n.Name, p.Name));
                            }

                        }

                        //Include只能用于导航属性的联合查询，他的参数是导航属性的名称
                        var a = context.Person.Where(r => r.Id == 1).Include("Nationality").SingleOrDefault();
                        //context.Entry(a).Property(p => p.Name).IsModified = true;
                        Console.WriteLine(string.Format("根据导航属性查询的国家:",a.Id,a.Name));
                        var b = context.Nationality.First().Persons.ToList();


                        scope.Complete();
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }
                
            }
            
        }
    }
}
