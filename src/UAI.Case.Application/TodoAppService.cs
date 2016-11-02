using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Common;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface ITodoAppService : IAppService<Todo>
    {
        long GetPendientes(Guid IdUsuario);
    }

    public class TodoAppService : AppService<Todo>, ITodoAppService
    {


        ITodoRepository _repo;
        public TodoAppService(ITodoRepository repository) : base(repository) { _repo = repository; }

        public long GetPendientes(Guid IdUsuario)
        {
            return _repo.GetPendientes(IdUsuario); 
        }
    }
}
