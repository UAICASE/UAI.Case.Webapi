using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IGrupoAppService : IAppService<Grupo>
    {
    }
    public class GrupoAppService : AppService<Grupo>, IGrupoAppService
    {
        public GrupoAppService(IRepository<Grupo> Repository) : base(Repository) { }
    } 
}
