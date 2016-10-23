using System;
using System.Collections.Generic;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.Proyectos

{
    
    public class Proyecto : Entity,  IAsignable
    {
        public Proyecto()
        {
          //  Revisiones = new List<Revision>();
          
        }

        public virtual EstadoProyecto Estado { get; set; }
        public virtual string Nombre { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual bool VisibleCurso { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual long Numero { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
