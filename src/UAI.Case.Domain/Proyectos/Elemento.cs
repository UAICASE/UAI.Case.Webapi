using System;
using System.Collections.Generic;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Interfaces;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Domain.Proyectos
{
    public   class Elemento : Entity, IAsignable
    {
        public virtual bool IsFolder { get;  set;}
        public virtual string Nombre { get; set; }
        public virtual IList<Elemento> Elementos { get; set; }
        //public virtual string DiagramId { get; set; }
        //public virtual Guid IdModelo { get; set; }
        public virtual Guid IdProyecto { get; set; } //cambia la cardinalidad, pero no tengo el proyecto sino el Id, es mas performante

        public virtual Usuario Usuario { get; set; }
        public virtual TipoDiagrama DiagramType { get; set; }


    }
}
