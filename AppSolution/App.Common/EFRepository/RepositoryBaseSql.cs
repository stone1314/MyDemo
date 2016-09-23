using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.EFRepository
{
   public class RepositoryBaseSql:IRepositoryBaseSql
    {
        private IUnitOfWork unitOfWork;

        public RepositoryBaseSql(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return unitOfWork;
            }
        }

        public void ExcuteSqlCommand(string sql, params object[] parameters)
        {
            ((IObjectContextAdapter)UnitOfWork.GetContext()).ObjectContext.ExecuteStoreCommand(sql, parameters);
        }

        public IEnumerable<R> SqlQuery<R>(string sql, params object[] parameters) where R : class
        {
            return ((IObjectContextAdapter)UnitOfWork.GetContext()).ObjectContext.ExecuteStoreQuery<R>(sql, parameters);
        }

        /// <summary>
        /// 执行函数或者存储过程
        /// </summary>
        /// <typeparam name="R">类型</typeparam>
        /// <param name="functionName">函数名称或者存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public R ExcuteFunction<R>(string functionName, params  ObjectParameter[] parameters)
        {
            var ret = ((IObjectContextAdapter)UnitOfWork.GetContext()).ObjectContext.ExecuteFunction<R>(functionName, parameters).FirstOrDefault();
            return ret;
                    
        }

        /// <summary>
        /// 执行函数或者存储过程
        /// </summary>
        /// <param name="functionName">存储过程名或函数名</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响行数</returns>
        public int ExcuteFunction(string functionName, params  ObjectParameter[] parameters)
        {
            var ret = ((IObjectContextAdapter)UnitOfWork.GetContext()).ObjectContext.ExecuteFunction(functionName, parameters);
            return ret;
        }



        public R ExcuteFunction<R>(string functionName, params System.Data.Objects.ObjectParameter[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExcuteFunction(string functionName, params System.Data.Objects.ObjectParameter[] parameters)
        {
            throw new NotImplementedException();
        }
                     
    }
}
