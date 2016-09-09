using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.EFRepository
{
    public class DbContextFactory : IDbContextFactory
    {
        private string _ConnectionString = string.Empty;
        private DbContext _dbContext;
        public DbContextFactory(string connectionString)
        {
            this._ConnectionString = connectionString;
        }

        private DbContext dbContext
        {
            get {
                if (this._dbContext == null)
                {
                    Type t = typeof(DbContext);
                    this._dbContext = (DbContext)Activator.CreateInstance(t, this._ConnectionString);
                }
                return _dbContext;
            }
        }

        public System.Data.Entity.DbContext GetDbContext()
        {
            return this.dbContext;
        }
    }
}
