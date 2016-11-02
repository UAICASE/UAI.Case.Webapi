using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Dto
{
    public class CreateProyectoDTO
    {
        public Guid Id { get; set; }
        public string Nombre {get; set; }
        public string Descripcion { get; set; }
    }
}
