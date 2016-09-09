using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.EFRepository
{
   public interface IUnitOfWork   : IDisposable
    {
        bool IsInTransaction { get; }

        void SaveChange();

        void SaveChange(SaveOptions saveOptions);

        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        void RollBacktransaction();

        void CommitTransaction();
        DbContext GetContext();
                      
    }
}
