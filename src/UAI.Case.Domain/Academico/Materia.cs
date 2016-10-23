using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Proyectos;
using UAI.Case.Domain.Roles;

namespace UAI.Case.Domain.Academico
{
    public class Materia : Entity
    {
        public Materia()
        {
            DiagramasValidos = new List<TipoDiagramaMateria>();
        }
        public virtual string Codigo { get; set; }
        public virtual string Nombre { get; set; }
        public virtual Docente Titular { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual IList<TipoDiagramaMateria> DiagramasValidos {get; set;}
        public virtual int Anio { get; set; }


    }
}
