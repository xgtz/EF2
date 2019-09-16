using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Test
{
     [TestFixture]
    public class UnitTestDate
    {
         [Test]
         public void TestDate()
         {
             DateTime dt = DateTime.Now;
             Console.WriteLine( string.Format("当天:{0}", dt.ToString("yyyy-MM-dd")));
             Console.WriteLine(string.Format("7日:{0}", dt.AddDays(-7).ToString("yyyy-MM-dd")));
             Console.WriteLine(string.Format("15天:{0}", dt.AddDays(-15).ToString("yyyy-MM-dd")));
         }

         [Test]
         public void TestDay0()
         {
             string beginDate = DateTime.Now.ToString("yyyy-MM-dd");
             string endDate = DateTime.Now.ToString("yyyy-MM-dd");

             DateTime dt1 = DateTime.ParseExact(beginDate, "yyyy-MM-dd",CultureInfo.InvariantCulture);
             DateTime dt2 = DateTime.ParseExact(beginDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

             TimeSpan sp = dt2.Subtract(dt1);
             Console.WriteLine(sp.Days);
         }


         [Test]
         public void TestDay7()
         {
             string beginDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
             string endDate = DateTime.Now.ToString("yyyy-MM-dd");

             DateTime dt1 = DateTime.ParseExact(beginDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
             DateTime dt2 = DateTime.ParseExact(beginDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

             TimeSpan sp = dt2.Subtract(dt1);
             Console.WriteLine(sp.Days);
         }


         [Test]
         public void TestDay15()
         {
             string beginDate = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd");
             string endDate = DateTime.Now.ToString("yyyy-MM-dd");

             DateTime dt1 = DateTime.ParseExact(beginDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
             DateTime dt2 = DateTime.ParseExact(beginDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

             TimeSpan sp = dt2.Subtract(dt1);
             Console.WriteLine(sp.Days);
         }

         [Test]
         public void TestDaySubstr()
         {
             string beginDate = DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd").Replace("-","");
             var str = beginDate.Substring(4);
             Console.WriteLine(beginDate);
             Console.WriteLine(str);
         }
    }
}
