using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Roles;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.Common
{
    public class Mail : Entity, IAsignable
    {
      
        public virtual Curso Curso { get; set; }
        public virtual DateTime? ReadDate { get; set; }
        public virtual Guid MailTo { get; set; }
        public virtual Usuario MailToUsuario { get; set; }
        public virtual String Subject { get; set; }
        public virtual String Body { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    
}

