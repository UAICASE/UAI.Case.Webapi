
using System;

using System.Linq;
using System.Linq.Expressions;

using UAI.Case.Application;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Interfaces;
using UAI.Case.Repositories;
using UAI.Case.Security;

namespace UAI.Case.Application
{
    public interface IAppService { } //por si se necesita un app service crudo

    public interface IAppService<T> where T : class
    {
        IAuthenticatedData AuthenticatedData { get; set; }
        void OnBeforeSaveOrUpdate(T entity);
        void OnAfterSaveOrUpdate(T entity);
        void OnBeforeDelete(T entity);
        T SaveOrUpdate(T Entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, Object>>[] fetchPaths);
        T Get(object id);
        T Get(object id, params Expression<Func<T, Object>>[] fetchPaths);
        void Delete(object id);
        void Delete(T entity);
        
    }

    public class AppService<T> : IAppService<T> where T : Entity // EntityWithTypedId<object>
    {

        

        private IAuthenticatedData _authenticatedData = null;
        public IAuthenticatedData AuthenticatedData
        {
            get
            {
                return _authenticatedData;
            }
            set
            {
                _authenticatedData = value;
            }
        }


        public virtual void OnBeforeSaveOrUpdate(T entity) {}
        public virtual void OnBeforeDelete(T entity) { }
        public virtual void OnAfterSaveOrUpdate(T entity) {



          

        }

    
       
        protected IRepository<T> Repository { get; set; }
        protected IUsuarioAppService _usuarioAppService = null;
        

        public AppService(IRepository<T> repository)
        {
           
            Repository = repository;
        }

        
        public T SaveOrUpdate(T entity)
        {
          
            OnBeforeSaveOrUpdate(entity);
            
            if (entity is Usuario)
            {
                if ((entity as Usuario).Id == Guid.Empty || (entity as Usuario).RequestedToLogin)
                {
                    if ((entity as Usuario).Password != null)
                        (entity as Usuario).Password = Cryptography.MD5Hash((entity as Usuario).Password);
                }

                //if ((entity as Usuario).NewPassword)
                //{
                //    (entity as Usuario).Password = Cryptography.MD5Hash((entity as Usuario).Password);
                //    (entity as Usuario).NewPassword = false; 
                //}

            }  
            Repository.SaveOrUpdate(entity);
            OnAfterSaveOrUpdate(entity);
            return entity;
        }


        public IQueryable<T> GetAll()
        {
                return Repository.GetAll();
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] fetchPaths)
        {
            return Repository.GetAll();
        }

        public T Get(object id)
        {
            if (id == null) return null;
            return Repository.Get(id);
        }

        public T Get(object id, params Expression<Func<T, object>>[] fetchPaths)
        {
            if (id == null) return null;
            return Repository.Get(id);
        }

        public void Delete(object id)
        {
            Delete(Repository.Get(id));
        }


        public void Delete(T entity)
        {
            OnBeforeDelete(entity);
            Repository.Delete(entity);
        }

        
    }
}
