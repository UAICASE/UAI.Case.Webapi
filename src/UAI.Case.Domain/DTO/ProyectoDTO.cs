using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;
using UAI.Case.Domain.Proyectos;

namespace UAI.Case.Dto
{
    public class ProyectoDTO : Entity
    {
        //public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        //public string FechaCreacion { get; set; }
        //public IList<Revision> Revisiones { get; set; }
        public Usuario Usuario { get; set; }
        public EstadoProyecto Estado { get; set; }
        public Curso Curso { get; set; }
        public Grupo Grupo { get; set; }
        public String Descripcion { get; set; }
        public IList<Elemento> Elementos { get; set; }
        public bool VisibleCurso { get; set; }


    }
}
