using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IClaseAppService : IAppService<Clase>
    {
    }

    public class ClaseAppService : AppService<Clase>, IClaseAppService
    {

        public ClaseAppService(IRepository<Clase> Repository) : base(Repository) { }
    }
}
