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
    //this.HasMany(r => r.Persons)表示一个国家中有很多人
    //.WithRequired(r => r.Nationality)是跟在HasMany(r => r.Persons)后面的，表示一个只能属于一个国家
    //.HasForeignKey(r => r.NationalityId)表示T_Nationality与T_Person表之间关联的外键是T_Person表中的NationalityId属性
    public class NationalityConfig : EntityTypeConfiguration<Nationality>
    {
        public NationalityConfig()
        {
            HasKey(k => k.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Name).HasColumnName("NAME");
            HasMany(r => r.Persons).WithRequired(r => r.Nationality).HasForeignKey(r => r.NationalityId);
            ToTable("T_NATIONALITY");
        }
    }
}
