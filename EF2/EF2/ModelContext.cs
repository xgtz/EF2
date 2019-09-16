namespace EF2
{
    using EF2.Model;
    using Oracle.ManagedDataAccess.Client;
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Linq;

    public partial class ModelContext : DbContext
    {

        public static string MsSqlConnectionString {
            get {
                return "data source=.;initial catalog=CodeFirst;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            }
        }
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

        public  DbSet<User> Users { get; set; }

        //public ModelContext()
        //    : base("name=MSSqlDbContext")
        //{
        //}


        public ModelContext()
            : base(MsSqlConnectionString)
        {
        }

        //public ModelContext()
        //    : base(OracleConnection,true)
        //{
        //}


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.HasDefaultSchema("DBO");
        }

     
    }


}