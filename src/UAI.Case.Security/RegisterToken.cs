using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Security
{
    public class RegisterToken
    {
        public Guid IdUsuario { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool IsValid()
        {
            return FechaVencimiento <= DateTime.Now;

        }
    }
}
