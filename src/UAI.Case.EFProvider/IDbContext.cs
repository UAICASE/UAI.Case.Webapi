using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;

namespace UAI.Case.EFProvider
{
    public interface IDbContext
    {
        void Commit();
        DbSet<T> Set<T>() where T:class;
        IQueryable<T> FromSQL<T>(string qry, params string[] parameters) where T : class;
        
        
    }
}
