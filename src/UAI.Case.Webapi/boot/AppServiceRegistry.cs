
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Repositories;
using UAI.Case.Security;

namespace UAI.Case.Boot
{
    public interface IUaiCaseController {

    }


    internal class AppServiceRegistry : Registry
    {
        public AppServiceRegistry()
        {
            Scan(a =>
            {

                a.AssemblyContainingType(typeof(IAppService<>));
                a.IncludeNamespace(typeof(IAppService<>).Namespace);
                a.WithDefaultConventions();
            });
           Policies.SetAllProperties(y => y.OfType<IAuthenticatedData>());
        }

    
    }

  




}
