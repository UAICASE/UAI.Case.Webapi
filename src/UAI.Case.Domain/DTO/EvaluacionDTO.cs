using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.CASE;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Dto
{
    public class EvaluacionDTO
    {   public Guid Id { get; set; }
        public  Usuario Usuario { get; set; }
        public Guid ModeloId { get; set; }
        public Guid DiagramaId { get; set; }
        public String Descripcion { get; set; }
        public EstadoEvaluacion Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public  IList<RespuestaEvaluacion> Respuestas { get; set; }

    }
}
