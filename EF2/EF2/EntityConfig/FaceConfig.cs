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
    public class FaceConfig : EntityTypeConfiguration<Face>
    {
        public FaceConfig()
        {
            HasKey(k => k.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Name).HasColumnName("NAME");
            // Property(p => p.TypeId).HasColumnName("TYPEID");
            Ignore(p => p.TypeId);

            HasOptional(t => t.Photo).WithRequired().Map(m => {
                m.MapKey("FACEID");
            });

            //A->WithRequiredDependent->B
            // 表示 A中包含B的不为null实例 ，A是主键实体  B是外键实体
            //A->WithRequiredPrincipal->B
            //表示A中包含B的不为null实例 ,A是外键实体 B是主键实体
            // WithOptionalDependent() 在a中有外键指向b的主键
            // WithOptionalPrincipal() 在b中有外键指向a的主键
            // WithRequired和WithOptional的区别为外键关系是否可空
            HasOptional(p => p.FaceType).WithMany(p=>p.Faces).Map(m => {
                m.MapKey("TYPEID");
            });

            
          
            ToTable("T_FACE");
        }
    }
}
