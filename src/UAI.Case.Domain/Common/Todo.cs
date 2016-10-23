using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.Common
{
    public class Todo : Entity
    {
        public virtual string Title { get; set; }
        public virtual bool Done { get; set; }
     //   public virtual Usuario Usuario { get; set; }
        public virtual Guid ChannelId { get; set; }
       public virtual bool EstadoAnterior { get; set; }
        

    }
}
