using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Repositories
{
    public interface IDbContext<T>
    {
        T Save(T entity);
        T Get(object id);
        IQueryable<T> GetAll();
        void Remove(T Entity);


    }
}
