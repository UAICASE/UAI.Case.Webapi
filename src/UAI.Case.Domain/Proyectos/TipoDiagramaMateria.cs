using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Domain.Proyectos
{
    public class TipoDiagramaMateria : Entity
    {
        TipoDiagrama _tipo;
        public TipoDiagramaMateria() { }
        public TipoDiagramaMateria( TipoDiagrama tipo) : this()
        {
            _tipo = tipo;
        }

        public virtual TipoDiagrama TipoDiagrama { get { return _tipo; } set { _tipo = value; } }
    }
}
