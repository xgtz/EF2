using EF2.Context;
using EF2.Model;
using EF2.Service;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace EF2.Test
{
    [TestFixture]
    public class UnitTest1
    {
        [SetUp]
        public void SetUp()
        {
            Database.SetInitializer<BFDbContext>(null);
            DbInterception.Add(new EF2.Behaviors.DatabaseLogger());
        }

        [Test]
        public void TestFaceSelect()
        {
            using (var scope = new TransactionScope())
            {
                
                using (var context = new BFDbContext())
                {
                    try
                    {

                        //var sw = new StreamWriter(@"d:\Data.log") { AutoFlush=true };
                        //context.Database.Log = sw.Write;

                        var face = context.Face.FirstOrDefault();
                       // var face = context.Face.Find(5);
                     
                        Assert.AreNotEqual(null, face);
                        Console.WriteLine(string.Format("FaceId:{0}", face.Id));
                        Console.WriteLine(string.Format("FaceName:{0}", face.Name));
                        Console.WriteLine(string.Format("TypeId:{0}", face.TypeId));
                        Console.WriteLine(string.Format("PhotoPath:{0}", face.Photo.Path));
                        Console.WriteLine(string.Format("TypeName:{0}", face.FaceType.Name));
                        scope.Complete();
                    }
                    catch (Exception e)
                    {
                        throw e;

                    }
                }
            }
            
        }


        [Test]
        public void TestFaceInsert()
        {
            using (var scope = new TransactionScope())
            {
                using (var context = new BFDbContext())
                {
                    var detched = context.Configuration.AutoDetectChangesEnabled;
                    context.Database.Log = Console.WriteLine;
                    try
                    {
                        //context.Configuration.UseDatabaseNullSemantics = false;
                        context.Configuration.AutoDetectChangesEnabled = false;
                        FaceType faceType = new FaceType() { Id=1,Name="自定义类型1" };
                        Photo photo = new Photo() { FaceId = 1, Path = "http://localhost:8088/testPDF/20190410170225.pdf" };
                        Face face = new Face()
                        {
                            Id = 1,
                            Name = "Face1",
                            TypeId = 1,
                            FaceType = faceType,
                            Photo = photo
                        };
                        context.Entry(face).State = EntityState.Added;
                        int c= context.SaveChanges();
                        Assert.Greater(c,0);
                        Console.WriteLine("保存数据的影响行数:{0}",c);
                        scope.Complete();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        [Test]
        public void TestFaceDelete()
        {
            using (var scope = new TransactionScope())
            {
                using (var context = new BFDbContext())
                {
                    context.Database.Log = Console.WriteLine;
                    var face = context.Face.FirstOrDefault();

                    context.Entry(face.Photo).State = EntityState.Deleted;
                    context.Entry(face).State = EntityState.Deleted;

                    int c = context.SaveChanges();
                    Assert.Greater(c, 0);
                    Console.WriteLine("删除数据的影响行数:{0}", c);
                    scope.Complete();
                    
                }
            }
        }

        [Test]
        public void TestFaceUpdate()
        {
            using (var scope = new TransactionScope())
            {
                using (var context = new BFDbContext())
                {
                    try
                    {

                        context.Database.Log = Console.WriteLine;
                        //var face = context.Face.AsNoTracking().FirstOrDefault();
                        var face = context.Face.FirstOrDefault();
                        face.Name = "修改后的名字";
                        //context.Face.Attach(face);
                        if (!context.ChangeTracker.HasChanges())
                        {
                            Console.WriteLine("实体对象没有发生变化!");
                        }
                        else
                        {
                            Console.WriteLine("实体对象发生了变化!");
                            context.Entry(face).State = EntityState.Modified;
                            int c = context.SaveChanges();
                            Assert.Greater(c, 0);
                            Console.WriteLine("修改数据的影响行数:{0}", c);
                        }
                        scope.Complete();
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        var entry = ex.Entries.Single();
                        entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                        context.SaveChanges();
                    }
                    
                }
            }
        }
    }
}
