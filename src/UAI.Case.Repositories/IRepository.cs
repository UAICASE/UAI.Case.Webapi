using System;
using System.Linq;


namespace UAI.Case.Repositories
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Provides a handle to application wide DB activities such as committing any pending changes,
        /// beginning a transaction, rolling back a transaction, etc.
        /// </summary>
        //IDbContext DbContext { get; }

        /// <summary>
        /// Returns null if a row is not found matching the provided Id.
        /// </summary>
        T Get(object id);
        

        
        IQueryable<T> GetAll();
        /// 
        /// Updating also allows you to commit changes to a detached object.  More info may be found at:
        /// http://www.hibernate.org/hib_docs/nhibernate/html_single/#manipulatingdata-updating-detached
        /// </summary>
        T SaveOrUpdate(T entity);

        /// <summary>
        /// I'll let you guess what this does.
        /// </summary>
        /// <remarks>The SharpLite.NHibernateProvider.Repository commits the deletion immediately; 
        /// see that class for details.</remarks>
        void Delete(T entity);
       
    }
}