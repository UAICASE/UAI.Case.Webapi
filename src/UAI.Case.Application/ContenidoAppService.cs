using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;


namespace UAI.Case.Application
{
    public interface IContenidoAppService : IAppService<Contenido>
    {
    }

    public class ContenidoAppService : AppService<Contenido>, IContenidoAppService
    {

        public ContenidoAppService(IRepository<Contenido> Repository) : base(Repository) { }
    }
}
