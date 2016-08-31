using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.IDAL
{
    public interface ICommonRepository<T> where T : class
    {
        bool Create(T model);

        bool Edit(T model);

        bool Delete(T model);

        int Delete(params object[] keyValues);

        T GetById(params object[] keyValues);

        IQueryable<T> GetList();

        IQueryable<T> GetList(Func<T, bool> whereLambda);

        bool IsExist(string id);

        int SaveChange();
    }
}
