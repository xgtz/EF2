using EF2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Configuration
{
    public class StudentConfiguration : EntityTypeConfiguration<Student>
    {
        public StudentConfiguration()
        {
            HasKey(k => k.ID);
            Property(p => p.ID).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Name).HasColumnName("NAME");
            Property(p => p.IsMale).HasColumnName("ISMALE");
            Property(p => p.StudentNumber).HasColumnName("STUDENTNUMBER");
            HasMany(t => t.Teachers).WithMany(s => s.Students).Map(m =>
            {
                m.MapLeftKey("STUDENTID");
                m.MapRightKey("TEACHERID");
                m.ToTable("STUDENT_TEACHER");
            });
            ToTable("STUDENT");
        }

    }
}
