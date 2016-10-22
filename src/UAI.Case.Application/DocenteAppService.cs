using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Roles;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IDocenteAppService : IAppService<Docente>
    {
    }
    public class DocenteAppService : AppService<Docente>, IDocenteAppService
    {
        public DocenteAppService(IRepository<Docente> repository) : base(repository) { }
    }
}
