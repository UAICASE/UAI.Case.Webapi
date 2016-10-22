using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface ICursoAppService : IAppService<Curso>
    {
    }
    public class CursoAppService : AppService<Curso> , ICursoAppService
    {
        public CursoAppService(IRepository<Curso> repository) : base(repository) { }
    }
}
