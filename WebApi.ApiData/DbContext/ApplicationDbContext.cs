using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkokieIceCream.Entity;

namespace SkokieIceCream.ApiData
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
            : base("DataContext")
        {
        }
        public new DbEntityEntry Entry(object entity)
        {
            return base.Entry(entity);
        }

        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry<TEntity>(entity);
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public override DbSet Set(Type entityType)
        {
            return base.Set(entityType);
        }

        public Database DataBaseInfo
        {

            get
            {
                return base.Database;
            }
        }

        public IEnumerable<T> ExecuteProcedure<T>(string procedureName, params object[] parameters)
        {
            return Database.SqlQuery<T>(procedureName, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sqlQuery, params object[] parameters)
        {
            return Database.ExecuteSqlCommandAsync(sqlQuery, parameters);
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
