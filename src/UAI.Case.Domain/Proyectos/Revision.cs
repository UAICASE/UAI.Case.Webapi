using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;

namespace UAI.Case.Domain.Proyectos
{
    public class Revision : Entity
    {
     
        public virtual DateTime Fecha { get; set; }
        public virtual Usuario Revisor { get; set; }
        public virtual String Comentarios { get; set; }
    }
}
