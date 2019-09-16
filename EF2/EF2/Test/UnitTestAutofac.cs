using Autofac;
using EF2.Business;
using EF2.Interface;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Test
{
    [TestFixture]
    public class UnitTestAutofac
    {
        private static IContainer Container { get; set; }

        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = builder.Build();

           
        }

        [Test]
        public void TestWriteDate()
        {
            using (var scope = Container.BeginLifetimeScope()) {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }
    }
}
