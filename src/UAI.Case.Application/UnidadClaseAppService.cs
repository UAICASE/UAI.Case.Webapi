using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
  

    public interface IUnidadClaseAppService : IAppService<UnidadClase>
    {
    }
    public class UnidadClaseAppService : AppService<UnidadClase>, IUnidadClaseAppService
    {

        public UnidadClaseAppService(IRepository<UnidadClase> Repository) : base(Repository) { }
    }

}
