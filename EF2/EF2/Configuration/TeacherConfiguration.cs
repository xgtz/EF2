using EF2.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Configuration
{
    public class TeacherConfiguration : EntityTypeConfiguration<Teacher>
    {
        public TeacherConfiguration()
        {
            HasKey(k => k.ID);
            //Property(p => p.ID).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnType("GUID");
            Property(p => p.ID).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Name).HasColumnName("NAME");
            ToTable("TEACHER");
        }
    }
}
