using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Domain.Common
{
    public class Numerador : Entity
    {
        
        public virtual string TipoEntidad { get; set; }

        
        public virtual long UltimoNumero { get; set; }

        public virtual Usuario Usuario { get; set; }
      

    }
}
