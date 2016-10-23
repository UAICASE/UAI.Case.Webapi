using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IContenidoMateriaAppService : IAppService<ContenidoMateria>
    {
    }

    public class ContenidoMateriaAppService : AppService<ContenidoMateria>, IContenidoMateriaAppService
    {

        public ContenidoMateriaAppService(IRepository<ContenidoMateria> Repository) : base(Repository) { }
    }
    
}
