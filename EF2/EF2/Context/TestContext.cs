using EF2.Configuration;
using EF2.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Context
{
    public class TestContext : DbContext
    {

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

       

        public static string OracleConnectionString {
            get {
                return "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=192.168.2.2)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));User ID=dbo;Password=7540000E;";
            }
        }

        public static DbConnection OracleConnection {

            get {
                DbConnection conn = new OracleConnection(OracleConnectionString);
                return conn;
            }
        }

        public TestContext()
            : base("name=OracleDbContext")
        {
        }

        //public TestContext()
        //    : base(OracleConnection,true)
        //{
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration()).Add(new TeacherConfiguration());
            modelBuilder.HasDefaultSchema("DBO");
            base.OnModelCreating(modelBuilder);
        }
    }
}
