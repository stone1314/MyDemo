using App.IDAL;
using App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL
{
    public abstract class CommonRepository<T> : ICommonRepository<T> where T : class
    {
        MyDBEntities db;

        public CommonRepository(MyDBEntities mydb)
        {
            this.db = mydb;
        }

        public MyDBEntities Context 
        { 
            get { return db; } 
        }

                            
        public bool Create(T model)
        {
            db.Set<T>().Add(model);
            return db.SaveChanges() > 0;
        }

        public bool Edit(T model)
        {
            if (db.Entry<T>(model).State == EntityState.Modified)
            {
                return db.SaveChanges() > 0;
            }
            else if (db.Entry<T>(model).State == EntityState.Detached)
            {
                try
                {
                    db.Set<T>().Attach(model);
                    db.Entry<T>(model).State = EntityState.Modified;
                }
                catch (InvalidOperationException err)
                { 
                    
                }
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public bool Delete(T model)
        {
            db.Set<T>().Remove(model);
            return db.SaveChanges() > 0;
        }

        public int Delete(params object[] keyValues)
        {
            T model = GetById(keyValues);
            if (model != null)
            {
                db.Set<T>().Remove(model);
                return  db.SaveChanges() ;
            }
            return -1;
        }

        public T GetById(params object[] keyValues)
        {
            return db.Set<T>().Find(keyValues);
        }

        public IQueryable<T> GetList()
        {
            return db.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetList(Func<T, bool> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).AsQueryable();
        }

        public bool IsExist(string id)
        {
            return GetById(id) != null;

        }

        public int SaveChange()
        {
            return db.SaveChanges();
        }



    }
}
