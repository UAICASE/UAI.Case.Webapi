using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Proyectos;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IElementoAppService : IAppService<Elemento>
    {
    }
    public class ElementoAppService : AppService<Elemento>, IElementoAppService
    {
        public ElementoAppService(IRepository<Elemento> repository) : base(repository){}
    }
}
