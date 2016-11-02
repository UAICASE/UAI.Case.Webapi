
using System.Collections.Generic;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Domain.Roles
{
    public class Alumno : Usuario
    {
        //public virtual IList<Curso> Alumnos { get; set; }
        public Alumno()
        {
            base.Rol = Rol.Alumno;
        }
    }
}
