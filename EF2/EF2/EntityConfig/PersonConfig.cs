using EF2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF2.EntityConfig
{
    public class PersonConfig : EntityTypeConfiguration<Person>
    {
        public PersonConfig()
        {
            HasKey(k => k.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Name).HasColumnName("NAME");
            Property(p => p.Age).HasColumnName("AGE");
            Property(p => p.NationalityId).HasColumnName("NATIONALITYID");
            HasRequired(r => r.Nationality).WithMany(r => r.Persons).HasForeignKey(r => r.NationalityId);
            //HasRequired(r => r.Nationality).WithMany(r => r.Persons).Map(m => m.MapKey("NATIONALITYID"));
            ToTable("T_PERSON");
            
        }
    }
}
