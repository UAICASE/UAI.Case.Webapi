using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.Common
{
    public abstract class Entity   {
        private int? requestedHashCode;



        public virtual DateTime? FechaEliminacion { get; set; }
        public virtual DateTime? FechaCreacion { get; set; }
        //public virtual Usuario Usuario { get; set; }
        public string Rev { get; set; }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get;  set; }

        

        public virtual bool Equals(Entity other)
        {
            if (null == other)
                return false;

            return ReferenceEquals(this, other) || other.Id.Equals(Id);
        }

        public virtual bool IsTransient()
        {
            return Equals(Id, default(Guid));
        }

        public virtual bool isDeleted()
        {
            return FechaEliminacion.Equals(null);
        }

        public override bool Equals(object obj)
        {
            var that = obj as Entity;
            return Equals(that);
        }

        public override int GetHashCode()
        {
            if (!requestedHashCode.HasValue)
                requestedHashCode = IsTransient() ? base.GetHashCode() : Id.GetHashCode();

            return requestedHashCode.Value;
        }
    }
    
}
