using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface INotaAppService : IAppService<Nota>
    {
    }
    public class NotaAppService : AppService<Nota>, INotaAppService
    {
        public NotaAppService(IRepository<Nota> repository) : base(repository) { }
    }
    
}
