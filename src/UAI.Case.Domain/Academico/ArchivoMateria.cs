using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;

namespace UAI.Case.Domain.Academico
{
    public class ArchivoMateria : Entity
    {
        public virtual Archivo Archivo { get; set; }
        public virtual Materia Materia { get; set; }
        public virtual String Descripcion { get; set; }

    }
}
