using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IMateriaAppService : IAppService<Materia>
    {
    }
    public class MateriaAppService : AppService<Materia>, IMateriaAppService
    {
        public MateriaAppService(IRepository<Materia> repository) : base(repository) {}
    }
}
