using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;
using UAI.Case.Domain.Roles;

namespace UAI.Case.Domain.Academico
{
    public class AlumnoCursoGrupo : Entity
    {
        public virtual Alumno Alumno { get; set; }
        public virtual Curso Curso{ get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual IList<Nota> Notas { get; set; }
        public virtual EstadoAfectacion Estado { get; set; }
    }
}
