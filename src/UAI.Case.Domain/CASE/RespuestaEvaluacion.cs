using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.CASE
{
    public class RespuestaEvaluacion : Entity, IAsignable
    {
        public virtual Usuario Usuario { get; set; }
        public virtual string Comentario { get; set; }
        public virtual Guid EvaluacionId { get; set; }
        public virtual Guid ModeloId { get; set; } //channel
    }
}
