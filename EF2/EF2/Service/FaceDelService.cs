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
    public class FaceDelService
    {
        public static void Test()
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    Database.SetInitializer<BFDbContext>(null);
                    using (var context = new BFDbContext())
                    {
                        context.Database.Log = Console.WriteLine;
                        var face = context.Face.FirstOrDefault();
                        context.Entry(face.Photo).State = EntityState.Deleted;
                        context.Entry(face.FaceType).State = EntityState.Deleted;
                        context.Entry(face).State = EntityState.Deleted;
                        int c = context.SaveChanges();
                        Console.WriteLine(string.Format("删除的影响行数:{0}", c));
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
