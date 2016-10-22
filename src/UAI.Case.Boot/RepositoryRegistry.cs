using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using UAI.Case.Repositories;
//using UAI.Case.Security;

namespace UAI.Case.Boot
{
    internal class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            Scan(a =>
            {

                //a.AssemblyContainingType(typeof(IRepository<>));
                //a.IncludeNamespace(typeof(IRepository<>).Namespace);
                a.WithDefaultConventions();
            });
            //Policies.SetAllProperties(y => y.OfType<IAuthenticatedData>());
        }


    }
}
