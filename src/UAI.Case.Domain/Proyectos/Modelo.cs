using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Domain.Proyectos
{
    public class Modelo : Entity
    {
        public virtual TipoDiagrama DiagramType { get; set; }
        public virtual string Type { get; set; }
        public virtual byte[] Model { get; set; }
        
    }
}
