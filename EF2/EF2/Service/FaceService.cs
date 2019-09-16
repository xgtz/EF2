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
    public class FaceService
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
                        FaceType faceType = new FaceType() { Id = 1, Name = "FaceType1" };
                        Photo photo = new Photo() { FaceId = 1, Path = "http://localhost:8088/testPDF/20190410170225.pdf" };
                        Face face = new Face()
                        {
                            Id = 1,
                            Name = "Face1",
                            TypeId = 1,
                            FaceType = faceType,
                            Photo = photo
                        };

                        //context.Entry(faceType).State = EntityState.Added;
                        context.Entry(face).State = EntityState.Added;
                        context.SaveChanges();

                        //foreach (var v in context.FaceType)
                        //{
                        //    Console.WriteLine(string.Format("FaceType:{0},{1}", v.Id, v.Name));
                        //}

                        //foreach (var v in context.Face)
                        //{
                        //    Console.WriteLine(string.Format("ID:{0},Name:{1},PhotoPath:{2}", v.Id, v.Name, v.Photo.Path));
                        //}

                        //foreach (var v in context.Face)
                        //{
                        //    v.FaceType = context.FaceType.Find(v.TypeId);
                        //    Console.WriteLine(string.Format("ID:{0},Name:{1},PhotoPath:{2},FaceType:{3}", v.Id, v.Name, v.Photo.Path, v.FaceType.Name));
                        //}

                        foreach (var v in context.Face)
                        {
                            Console.WriteLine(string.Format("ID:{0},Name:{1},PhotoPath:{2},FaceType:{3},TYPEID:{4}", v.Id, v.Name, v.Photo.Path, v.FaceType.Name,v.TypeId ));
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
