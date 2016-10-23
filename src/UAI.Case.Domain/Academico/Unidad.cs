using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;

namespace UAI.Case.Domain.Academico
{
    public class Unidad : Entity
    {
        public virtual string Identificador { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual IList<Contenido> Contenidos { get; set; }
        public virtual IList<Unidad> Unidades { get; set; }
    }
}
