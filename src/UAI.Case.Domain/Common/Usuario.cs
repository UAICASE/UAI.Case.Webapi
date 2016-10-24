using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Enums;
using UAI.Case.Domain.Roles;

namespace UAI.Case.Domain.Common
{
    
    public class Usuario : Entity
    
    {
        
        public virtual string Apellido { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Mail { get; set; }
        public virtual string Password { get; set; }
        public virtual bool Active { get; set; }
        public virtual bool RequestedToLogin {get { return (Password != null & !Active); }}
        public virtual string RegisterToken { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual DateTime? FechaNacimiento { get; set; }
        public virtual long Legajo { get; set; }
        public virtual string Perfil { get; set; }
        public virtual Guid IdRegisterRequestUserId { get; set; }
    }
}
