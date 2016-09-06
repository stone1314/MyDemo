using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Common.EFRepository
{
    interface IRepositoryBaseSql
    {
        /// <summary>
        /// 对数据库执行的DDL
        /// </summary>
        /// <param name="sql">命令字符串</param>
        /// <param name="parameters">字符串对应的参数</param>
        void ExcuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// 类型可以是包含与从查询返回的列名匹配的属性的任何类型，也可以是简单的基元类型
        /// </summary>
        /// <typeparam name="R">查询所返回对象的类型</typeparam>
        /// <param name="sql">SQL 查询字符串</param>
        /// <param name="parameters">要应用于SQL查询字符串的参数</param>
        /// <returns></returns>
        IEnumerable<R> SqlQuery<R>(string sql, params object[] parameters) where R : class;

        /// <summary>
        /// 执行函数或者存储过程
        /// </summary>
        /// <typeparam name="R">类型</typeparam>
        /// <param name="functionName">函数名称或者存储过程名称</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        R ExcuteFunction<R>(string functionName, params ObjectParameter[] parameters);

        /// <summary>
        /// 执行函数或存储过程（不返回结果集，只返回影响行数）
        /// </summary>
        /// <param name="functionName">存储过程名</param>
        /// <param name="parameters">参数</param>
        /// <returns>影响行数</returns>
        int ExcuteFunction(string functionName, params ObjectParameter[] parameters);

        /// <summary>
        /// UnitOfWork 对象
        /// </summary>
        IUnitOfWork UntiOfWork { get; }
    
    
    
    
    
    }
}
