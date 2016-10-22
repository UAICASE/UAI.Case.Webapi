
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

namespace UAI.Case.Domain.Roles
{
    public class Admin : Usuario
    {
        public Admin()
        {
            base.Rol = Rol.Admin;
        }
    }
}
