using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.Common
{
    public class RequestRegisterToken : Entity, IAsignable
    {

        public RequestRegisterToken()
        {
            FechaVencimiento = DateTime.Now.AddHours(12);
        }
        public virtual DateTime FechaVencimiento {get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Boolean Actived { get; set; }
        public virtual string Mail { get; set; }
        public virtual bool IsValid()
        {
            return (FechaVencimiento > DateTime.Now) && !Actived;
        }
    } 
}
