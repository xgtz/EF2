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
    public class FaceTypeConfig : EntityTypeConfiguration<FaceType>
    {
        public FaceTypeConfig()
        {
            HasKey(k => k.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Name).HasColumnName("NAME");
            ToTable("T_FACETYPE");
        }
    }
}
