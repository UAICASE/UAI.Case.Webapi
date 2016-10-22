using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;

namespace UAI.Case.Repositories
{
    public class UaiCaseContext<T> : IDbContext<T> where T : Entity
    {
        IList<T> list = new List<T>();
        public T Get(object id)
        {
            return list.Where(o => o.Id.Equals(id)).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {

            return list.AsQueryable();
        }

        public void Remove(T Entity)
        {
            list.Remove(Entity);
        }

        public T Save(T entity)
        {
            if (entity.Id.Equals(Guid.Empty))
                entity.Id = Guid.NewGuid();
            
            list.Add(entity);
            return entity;
        }
    }
}
