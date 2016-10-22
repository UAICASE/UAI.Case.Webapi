using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Enums;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.Common
{
    public  class LogMessage : Entity, IAsignable

    {
        public virtual Guid IdDestino { get; set; }
        public virtual string Mensaje { get; set; }
        public virtual string Mensaje2 { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual TipoTagLog Tag { get; set; }

    }
}
