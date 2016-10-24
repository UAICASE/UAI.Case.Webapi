
using System;
using System.Linq;
using System.Linq.Expressions;
using UAI.Case.Domain;
using Microsoft.AspNetCore.Http;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Interfaces;
using System.Text;
using UAI.Case.Security;
using UAI.Case.EFProvider;


namespace UAI.Case.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
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

       // private readonly ISessionFactory _sessionFactory;
        IHttpContextAccessor _context;
       protected IDbContext _db;
        
        public Repository(IHttpContextAccessor context, IDbContext db)
        {
            
            _db= db;
            _context = context;


        }
        
        public void Delete(T entity)
        {

            
            //TODO: pasar esto a un NH event listener y ver como se puede poner un defautl where deleted=0
            entity.FechaEliminacion = DateTime.Now;
            //_db.Remove(entity);
            _db.Set<T>().Remove(entity);
            _db.Commit();
            
        }



        public T Get(object id)
        {
              var o = _db.Set<T>().Where(p=>p.Id.Equals(id)).FirstOrDefault();

            return o;
        }

       
        public IQueryable<T> GetAll()
        {

            var col = _db.Set<T>();
            return col;


            //return _db.Set<T>();
        }

     
            

        public T SaveOrUpdate(T entity)
        {

         
                if (entity.IsTransient())
            {

                entity.FechaCreacion = DateTime.Now;
            }

                if (entity is IAsignable && _authenticatedData!=null)
               
                    if (_authenticatedData.UsuarioId != null)
                    {
                                        
                    Usuario usuario = null;
                    
                    var asignable = ((IAsignable)entity);

                    if (asignable.Usuario == null)
                    {
                        usuario = new Usuario();// _users.Get(_authenticatedData.UsuarioId);//.Where(p=>p.Id.Equals(_authenticatedData.UsuarioId)).FirstOrDefault();
                        
                        asignable.Usuario = usuario;
                    }

                    }

            _db.Set<T>().Add(entity);
            _db.Commit();
//            _db.SaveOrUpdate(entity);
            return entity;
        }


    }
}
