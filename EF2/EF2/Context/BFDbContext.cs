using EF2.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EF2.Context
{
    // DbContext是负责与数据进行交互的主要类,他是域或实体类与数据库之间的桥梁
    // 1 EntitySet:DbContext包含映射到数据库表的所有实体的实体集(DbSet<TEntity>)
    // 2.查询:DbContext将Linq-to-eitities转化成sql查询并发送的数据库执行
    // 3.变化跟踪:DbContext对数据库查询出的实体的变化进行跟踪
    // 4.持久数据:DbContext依据实体的状态对数据库进行插入修改和删除
    // 5.缓存:DbContext默认一级缓存。它存在在上下文声明周期内已经被检索的实体
    // 6.管理关系:DbContext通过DbFirst,codeFirst和Fluent api等方式管理管理关系
    // 7.对象实现:DbContext将原始表数据转化成实体对象
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
            // 在 Code First Conventions 下是这样移除级联删除的
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            Database.SetInitializer<BFDbContext>(null);

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
