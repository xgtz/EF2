namespace EF2
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using EF2.Model;
    using EF2.Configuration;

    public partial class MySqlContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public MySqlContext()
            : base("name=MySqlContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration()).Add(new StudentConfiguration()).Add(new TeacherConfiguration());
            modelBuilder.HasDefaultSchema("DBO");
            base.OnModelCreating(modelBuilder);
        }
    }
}
