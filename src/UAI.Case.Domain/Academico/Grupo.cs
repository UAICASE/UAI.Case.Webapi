using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Interfaces;
using UAI.Case.Domain.Roles;

namespace UAI.Case.Domain.Academico
{
    public class Grupo : Entity, IAsignable
    {
        public Grupo()
        {
            
        }
        public virtual Curso Curso {get; set;}
        public virtual String Identificador { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual int Maximo { get; set; }
       
    }
}
