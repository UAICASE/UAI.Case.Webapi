using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Domain.Academico
{
    public class UnidadClase : Entity
    {
        public virtual Guid ClaseId { get; set; }
        public virtual Unidad Unidad{ get; set; }
        
    }
}
