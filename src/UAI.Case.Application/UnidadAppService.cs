using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
   

        public interface IUnidadAppService : IAppService<Unidad>
        {
        }

        public class UnidadAppService : AppService<Unidad>, IUnidadAppService
        {

            public UnidadAppService(IRepository<Unidad> Repository) : base(Repository) { }
        }
   
}
