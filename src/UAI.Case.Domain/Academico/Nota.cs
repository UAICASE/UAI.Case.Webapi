using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Domain.Academico
{

   
    public class Nota: Entity
    {
        public virtual double Valor { get; set; }
        public virtual TipoNota TipoNota { get; set; }
        public virtual string Observaciones { get; set; }
    }
}
