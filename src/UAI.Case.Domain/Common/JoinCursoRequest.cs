using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Enums;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.Common
{
    public class JoinCursoRequest : Entity, IAsignable
    {
        public virtual Usuario Usuario { get; set; }
       
        public virtual Guid CursoId { get; set; }
        public virtual EstadoAfectacion Estado { get; set; }
        public JoinCursoRequest()
        {
            Estado = EstadoAfectacion.Pendiente;
        }
       
    }
}
