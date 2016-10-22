using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Domain.Academico
{
    public class Clase : Entity
    {
        public virtual TipoClase Tipo { get; set; }
        public virtual DateTime Fecha { get; set; }
        public virtual int Numero { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual IList<UnidadClase> Unidades { get; set; }
        public virtual Curso Curso { get; set; }
    }
}
