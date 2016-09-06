using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.EFRepository
{
    interface IUnitOfWork   : IDisposable
    {
        bool IsInTranscation { get; }

        void SaveChange();

        void SaveChange(SaveOptions saveOptions);

        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);
    }
}
