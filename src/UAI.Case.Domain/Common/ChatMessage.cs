using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.Common
{
    public class ChatMessage : Entity, IAsignable
    {
        public virtual Guid IdDestino { get; set; }
        public virtual string Mensaje { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual string ReadedBy { get; set; }

        public virtual bool IsReaded(Guid id)
        {
            if(ReadedBy!=null)
            return ReadedBy.Contains(id.ToString());
            return false;
        }
    }
}
