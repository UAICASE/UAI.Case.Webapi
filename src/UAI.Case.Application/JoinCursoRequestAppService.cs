using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Common;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    
    public interface IJoinCursoRequestAppService : IAppService<JoinCursoRequest>
    {
    }
    public class JoinCursoRequestAppService : AppService<JoinCursoRequest>, IJoinCursoRequestAppService
    {
        public JoinCursoRequestAppService(IRepository<JoinCursoRequest> repository) : base(repository) { }
    }
  
}
