using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Proyectos;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IProyectoAppService : IAppService<Proyecto>
    {
    }
    public class ProyectoAppService : AppService<Proyecto>, IProyectoAppService
    {
        public ProyectoAppService(IRepository<Proyecto> proyectoRepository) : base(proyectoRepository)
        {

        }
       
    }
}
