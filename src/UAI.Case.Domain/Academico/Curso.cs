using System.Collections.Generic;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;
using UAI.Case.Domain.Roles;

namespace UAI.Case.Domain.Academico
{
    public class Curso : Entity
    {
        public virtual Materia Materia { get; set; }
        public virtual Docente Docente { get; set; }
        public virtual TipoVisibilidadCurso TipoVisibilidad { get; set; }
        public virtual Dia Dia { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual Sede Sede { get; set; }
        public virtual TipoComision Comision { get; set; }
        public virtual bool Activo { get; set; }
        public virtual int Anio { get; set; }
    }
}
