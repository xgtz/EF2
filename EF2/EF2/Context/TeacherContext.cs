using EF2.Configuration;
using EF2.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Context
{
    public class TeacherContext : DbContext
    {
         public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public TeacherContext()
            : base("name=OracleDbContext")
        {
        }

        public TeacherContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        { 
        
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration()).Add(new TeacherConfiguration());
            modelBuilder.HasDefaultSchema("DBO");
            base.OnModelCreating(modelBuilder);
        }
    }
}
