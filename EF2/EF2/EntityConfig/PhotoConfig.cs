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
    public class PhotoConfig : EntityTypeConfiguration<Photo>
    {
        public PhotoConfig()
        {
            HasKey(k => k.FaceId);
            Property(p => p.FaceId).HasColumnName("FACEID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Path).HasColumnName("PATH");
            //HasRequired(p => p.Face).WithOptional(t => t.Photo);
            HasRequired(t => t.Face).WithOptional(t => t.Photo).WillCascadeOnDelete(true);

            ToTable("T_PHOTO");
        }
    }
}
