﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.EFRepository
{
   public class UnitOfWork :IUnitOfWork
    {
        private DbTransaction _transaction;
        private DbContext _dbContext;

        public UnitOfWork(IDbContextFactory dbFactory)
        {
            _dbContext = dbFactory.GetDbContext();            
        }

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        public bool IsInTransaction
        {
            get { return _transaction != null; }
        }

        public void SaveChanges()
        {
            if (IsInTransaction)
            {
                throw new ApplicationException("A transaction is running. Call CommitTransaction instead.");
            }
            ((IObjectContextAdapter)_dbContext).ObjectContext.SaveChanges();

        }

        public void SaveChange(System.Data.Objects.SaveOptions saveOptions)
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction(System.Data.IsolationLevel isolationLevel)
        {
            if (_transaction != null)
            {
                throw new ApplicationException("Cannot begin a new transaction while an existing transaction is still running. " +
                                                "Please commit or rollback the existing transaction before starting a new one.");
            }
            OpenConnection();

        }



        public void RollBackTransaction()
        {
            if (_transaction == null)
            {
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            }

            if (IsInTransaction)
            {
                _transaction.Rollback();
                ReleaseCurrentTransaction();
            }

        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
            }

            try
            {
                ((IObjectContextAdapter)_dbContext).ObjectContext.SaveChanges();
                _transaction.Commit();
                ReleaseCurrentTransaction();
            }
            catch
            {
                RollBackTransaction();
                throw;
            }       
        }


        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        /// <summary>
        /// Disposes off the managed and unmanaged resources used.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._dbContext != null)
                {
                    this._dbContext.Dispose();
                    this._dbContext = null;
                    GC.SuppressFinalize(this);
                    //System.Diagnostics.Trace.Write(new Exception("unitofwork:" + this.ToString()), "error");
                }
            }
        }
        #endregion

        private void OpenConnection()
        {
            if (((IObjectContextAdapter)_dbContext).ObjectContext.Connection.State != ConnectionState.Open)
            {
                ((IObjectContextAdapter)_dbContext).ObjectContext.Connection.Open();
            }
        }

        /// <summary>
        /// Releases the current transaction
        /// </summary>
        private void ReleaseCurrentTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }


        public DbContext GetContext()
        {
            return this._dbContext;
        }

        public bool IsInTranscation
        {
            get { throw new NotImplementedException(); }
        }

        public void SaveChange()
        {
            throw new NotImplementedException();
        }

        public void RollBacktransaction()
        {
            throw new NotImplementedException();
        }
    }
}
