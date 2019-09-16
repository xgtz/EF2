using EF2.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Context
{
    public class BFDbContext :DbContext
    {
        public BFDbContext() :
            base("name=OracleDbContext") 
        {
       
            //Configuration.AutoDetectChangesEnabled = false;//关闭自动跟踪对象的属性变化
            //Configuration.LazyLoadingEnabled = false; //关闭延迟加载
            //Configuration.ProxyCreationEnabled = false; //关闭代理类
            //Configuration.ValidateOnSaveEnabled = false; //关闭保存时的实体验证
            Configuration.UseDatabaseNullSemantics = true; //关闭数据库null比较行为
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema("DBO");
            
            base.OnModelCreating(modelBuilder);
           
        }


        public DbSet<Nationality> Nationality { get; set; }

        public DbSet<Person> Person { get; set; }

        public DbSet<Face> Face { get; set; }

        public DbSet<Photo> Photo { get; set; }

        public DbSet<FaceType> FaceType { get; set; }
    }
}
