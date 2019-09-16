using EF2.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace EF2.Service
{
    public class BFDbContextDelService
    {
        public static void Test()
        { 
            using(var scope = new TransactionScope() )
            {
                Database.SetInitializer<BFDbContext>(null);
                using (var context = new BFDbContext())
                {
                    context.Database.Log = Console.WriteLine;
                    try
                    {
                        var p = context.Person.Find(1);
                        Console.WriteLine(string.Format("{0}---{1}", p.Nationality.Id, p.Nationality.Name));

                        context.Entry(p.Nationality).State = EntityState.Deleted;
                        context.Entry(p).State = EntityState.Deleted;
                        
                        int c =  context.SaveChanges();
                        
                        Console.WriteLine(string.Format("删除人员信息的返回值:{0}", c));
                        foreach (var n in context.Nationality)
                        {
                            Console.WriteLine(string.Format("国家信息--ID:{0},Name:{1}", n.Id, n.Name));
                        }
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
