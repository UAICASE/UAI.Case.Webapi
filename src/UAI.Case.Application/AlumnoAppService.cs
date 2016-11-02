using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Application;
using UAI.Case.Domain.Roles;
using UAI.Case.Repositories;

namespace UAI.Case.Application
{
    public interface IAlumnoAppService : IAppService<Alumno>
    {
    }
    public class AlumnoAppService : AppService<Alumno>, IAlumnoAppService
    {
        public AlumnoAppService(IRepository<Alumno> repository) : base(repository) { }
    }
}
