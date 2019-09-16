using EF2.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Test
{
    [TestFixture]
    public class UnitTestJoin
    {
        public List<DBook> lstBook = new List<DBook>();
        public List<DCategory> lstCategory = new List<DCategory>();
        [SetUp]
        public void SetUp()
        {
            lstBook.AddRange(new List<DBook>() { 
                new DBook(){ Name="ASP.NET MVC 商业应用开发",CategoryId=1 },
                new DBook(){ Name="ASP.NET MVC 实用精髓",CategoryId=1 },
                new DBook(){ Name="EF 实用精髓",CategoryId=2 },
                new DBook(){ Name="C# 实用精髓",CategoryId=2 },
                new DBook(){ Name="HTML5 实用精髓",CategoryId=3 },
                new DBook(){ Name="HTML5 完美风暴",CategoryId=4 }
            });

            lstCategory.AddRange(new List<DCategory>() { 
                new DCategory(){Name="ASP.NET",Id=1},
                new DCategory(){Name=".Net",Id=2},
                new DCategory(){Name="Web",Id=3},
                new DCategory(){Name="Java",Id=5},
            });
           
        }

        [Test]
        public void TestJoin()
        {
            var cbooks = from c in lstCategory
                         join b in lstBook
                         on c.Id equals b.CategoryId
                         select new { 
                            BookCategory = c.Name,
                            BookTitle = b.Name
                         };
            foreach (var book in cbooks)
            {
                Console.WriteLine(string.Format("书籍分类:{0}\t书名:{1}", book.BookCategory, book.BookTitle));
            }
        }

        [Test]
        public void TestLeftJoin()
        {

            var cbook = from b in lstBook
                        join c in lstCategory
                        on b.CategoryId equals c.Id
                        into gr
                        from ur in gr.DefaultIfEmpty()
                        select new { 
                            CategoryId= b.CategoryId,
                            BookName = b.Name,
                            Category = ur == null ? "" : ur.Name
                        };
            foreach (var cb in cbook)
            {
                Console.WriteLine(string.Format("{0}\t{1}\t{2}", cb.CategoryId, cb.BookName, cb.Category));
            }


            //var cbooks = from c in lstCategory
            //             join b in lstBook
            //             on c.Id equals b.CategoryId
            //             into bgroup
            //             select bgroup;
            //foreach (var cb in cbooks)
            //{
            //    foreach (var c in cb)
            //    {
            //        Console.WriteLine(string.Format("{0}\t{1}", c.Name, c.CategoryId));
            //    }
            //}
                          
        }

        [Test]
        public void TestRightJoin()
        {
            var cbook = from c in lstCategory
                        join b in lstBook on c.Id equals b.CategoryId
                        into gr
                        from ur in gr.DefaultIfEmpty()
                        select new
                        {
                            BookName = ur == null ? "" : ur.Name,
                            BookCategory = ur == null ? "" : ur.CategoryId.ToString(),
                            Categror = c.Id
                        };
            foreach (var cb in cbook)
            {
                Console.WriteLine(string.Format("{0}\t{1}\t{2}", cb.BookName, cb.BookCategory, cb.Categror));
            }
        }

        [Test]
        public void TestJoinMethod()
        {
            var cbooks = lstCategory.Join(lstBook, 
                            c => c.Id, 
                            b => b.CategoryId, 
                            (c, b) => new {
                                BookCategory = c.Name,
                                BookTitle = b.Name
                            });
            foreach (var book in cbooks)
            {
                Console.WriteLine(string.Format("书籍分类:{0}\t书名:{1}", book.BookCategory, book.BookTitle));
            }
        }

     

        [Test]
        public void TestGroupJoin()
        {
            IEnumerable<IEnumerable<DBook>> cbooks = from c in lstCategory
                                                     join b in lstBook
                                                     on c.Id equals b.CategoryId
                                                     into bgroup
                                                     select bgroup;
            foreach (var cb in cbooks)
            {
                foreach (var c in cb)
                {
                    Console.WriteLine(string.Format("{0}\t{1}", c.Name, c.CategoryId));
                }
            }
        }

        [Test]
        public void TestGroupJoinMethod()
        {
            var cbooks = lstCategory.GroupJoin(
                        lstBook,
                        c => c.Id,
                        b => b.CategoryId,
                        (c, bgroup) => new {
                            GCategory = c.Name,
                            GBook = bgroup
                        });
            foreach (var cb in cbooks)
            {
                Console.WriteLine(string.Format("\n{0}\n", cb.GCategory));
                foreach (var c in cb.GBook)
                {
                    Console.WriteLine(string.Format("{0}\t{1}", c.Name, c.CategoryId));
                }
            }
        }

        [Test]
        public void TestLeftGroupJoinMethod()
        {
            var cbooks = lstBook.GroupJoin(
                    lstCategory,
                    b => b.CategoryId,
                    c => c.Id,
                    (b, listtp) => listtp.DefaultIfEmpty(new DCategory()).Select(z => new
                    {
                        CategoryId = b.CategoryId,
                        BookTitle = b.Name,
                        CategoryName = null == z ? "":z.Name
                    })
                    );
            foreach (var cb in cbooks)
            {
                foreach (var p in cb)
                {
                    Console.WriteLine(string.Format("{0}\t{1}\t{2}\t", p.CategoryId, p.BookTitle, p.CategoryName));
                }
            }

        }

        [Test]
        public void TestRightGroupJoinMethod()
        {
            var cbooks = lstCategory.GroupJoin(
                    lstBook,
                    c => c.Id,
                    b => b.CategoryId,
                    (c, g) => g.DefaultIfEmpty(new DBook()).Select(z => new
                    {
                        CategoryId = null == z ? "" : z.CategoryId.ToString(),
                        BookTitle = null == z ? "" : z.Name,
                        CaregoryName = c.Name
                    }));
            foreach (var cb in cbooks)
            {
                foreach (var p in cb)
                {
                    Console.WriteLine(string.Format("{0}\t{1}\t{2}\t", p.CategoryId, p.BookTitle, p.CaregoryName));
                }
            }

        }

        [Test]
        public void TestGroup()
        { 
        
        }
    }
}
