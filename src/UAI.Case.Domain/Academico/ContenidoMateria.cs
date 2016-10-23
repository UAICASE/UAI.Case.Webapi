using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;

namespace UAI.Case.Domain.Academico
{
    public class ContenidoMateria : Entity
    {
        public virtual Materia Materia { get; set; }
        public virtual Unidad Unidad { get; set; }
    }
}
