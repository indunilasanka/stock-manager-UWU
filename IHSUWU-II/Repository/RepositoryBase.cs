using Login.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Runtime.InteropServices;
using System.Data.Entity;


namespace Login.Repository
{
    public class RepositoryBase<C> : IDisposable
   where C : UserContext, IDisposedTracker, new()
    {
        private C _DataContext;

        public virtual C DataContext
        {
            get
            {
                if (_DataContext == null || _DataContext.IsDisposed)
                {
                    _DataContext = new C();
                    AllowSerialization = true;
                    //Disable ProxyCreationDisabled to prevent the "In order to serialize the parameter, add the type to the known types collection for the operation using ServiceKnownTypeAttribute" error
                }
                return _DataContext;
            }
        }

        public virtual bool AllowSerialization
        {
            get
            {
                //return ((IObjectContextAdapter) _DataContext)
                //.ObjectContext.ContextOptions.ProxyCreationEnabled = false;
                return _DataContext.Configuration.ProxyCreationEnabled;
            }
            set
            {
                _DataContext.Configuration.ProxyCreationEnabled = !value;
            }
        }

        public virtual T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            if (predicate != null)
            {
                using (DataContext)
                {
                    return DataContext.Set<T>().Where(predicate).SingleOrDefault();
                }
            }
            else
            {
                throw null;
            }
        }

        public virtual IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return DataContext.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TKey>> orderBy) where T : class
        {
            return GetList(predicate).OrderBy(orderBy);
        }

        public virtual IQueryable<T> GetList<T, TKey>(Expression<Func<T, TKey>> orderBy) where T : class
        {
            return GetList<T>().OrderBy(orderBy);
        }

        public virtual IQueryable<T> GetList<T>() where T : class
        {
            return DataContext.Set<T>();
        }

        public virtual void Save<T>(T entity) where T : class
        {
            DataContext.SaveChanges();
        }

        public virtual void Insert<T>(T entity) where T : class
        {
            DataContext.Set<T>().Add(entity);
        }

        public virtual void Update<T>(T entity, params string[] propsToUpdate) where T : class
        {
            DataContext.Set<T>().Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }
        public virtual void Delete<T>(T entity, params string[] propsToUpdate) where T : class
        {


            DataContext.Set<T>().Attach(entity);
            DataContext.Entry(entity).State = EntityState.Modified;
            DataContext.Set<T>().Remove(entity);
            DataContext.SaveChanges();
        }

        public void ExecuteStoreCommand(string cmdText, params object[] parameters)
        {
            DataContext.Database.ExecuteSqlCommand(cmdText, parameters);
        }


        public void Dispose()
        {
            if (DataContext != null) DataContext.Dispose();
        }
    }
}
