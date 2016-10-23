using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UAI.Case.Domain.Interfaces;

namespace UAI.Case.Domain.Common
{
    public abstract class Entity : EntityWithTypedId<Guid>
    { }

    
    public abstract class EntityWithTypedId<TId>
    {
        private int? requestedHashCode;



        public virtual DateTime? FechaEliminacion { get; set; }
        public virtual DateTime? FechaCreacion { get; set; }
        //public virtual Usuario Usuario { get; set; }

        public virtual TId Id { get;  set; }

        //public virtual int Version { get; private set; }

        public virtual bool Equals(EntityWithTypedId<TId> other)
        {
            if (null == other)
                return false;

            return ReferenceEquals(this, other) || other.Id.Equals(Id);
        }

        public virtual bool IsTransient()
        {
            return Equals(Id, default(TId));
        }

        public virtual bool isDeleted()
        {
            return FechaEliminacion.Equals(null);
        }

        public override bool Equals(object obj)
        {
            var that = obj as EntityWithTypedId<TId>;
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
