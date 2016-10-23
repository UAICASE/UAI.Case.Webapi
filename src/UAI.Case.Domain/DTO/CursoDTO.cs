using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Enums;
using UAI.Case.Domain.Roles;

namespace UAI.Case.Dto
{
    public class CursoDTO: IEquatable<CursoDTO>
    {
        public Guid Id { get; set; }

        public int Anio { get; set; }
        public Materia Materia { get; set; }
        public Dia Dia { get; set; }
        public Sede Sede { get; set; }
        public Turno Turno { get; set; }
        public Docente Docente { get; set; }
        public bool Activo { get; set; }
        public Grupo Grupo { get; set; }
        public TipoComision Comisiom { get; set; }
        public bool Mio { get; set; }

        public TipoVisibilidadCurso TipoVisibilidad{ get; set; }
        public bool Equals(CursoDTO other)
        {
            return Id.Equals(other.Id);
        }


    }
}
