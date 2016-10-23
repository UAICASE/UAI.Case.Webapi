using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Roles;

namespace UAI.Case.Dto
{
    public class GrupoDTO
    {
        public Grupo Grupo { get; set; }
        public IList<Alumno> Alumnos { get; set; }
    }
}
