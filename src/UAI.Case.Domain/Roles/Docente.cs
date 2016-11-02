using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Domain.Roles
{
    public class Docente : Usuario
    {
        public Docente()
        {
            base.Rol = Rol.Docente;
        }
    }
}
