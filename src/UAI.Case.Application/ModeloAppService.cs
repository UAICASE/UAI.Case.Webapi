using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Proyectos;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IModeloAppService : IAppService<Modelo>
    {
    }
    public class ModeloAppService : AppService<Modelo>, IModeloAppService
    {
        public ModeloAppService(IRepository<Modelo> repository) : base(repository) { }
    }
}
