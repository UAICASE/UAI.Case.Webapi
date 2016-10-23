using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.CASE
{
    public class Evaluacion : Entity, IAsignable
    {
        public virtual Usuario Usuario { get; set; }
        public virtual Guid ModeloId { get; set; }
        public virtual Guid DiagramaId { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual EstadoEvaluacion Estado { get; set; }
//        public virtual IList<RespuestaEvaluacion> Respuestas { get; set; }
    
    }
}
