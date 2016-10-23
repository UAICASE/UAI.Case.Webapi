using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Dto
{
    public abstract class DtoBase : IEquatable<DtoBase>
    {
        public Guid Id { get; set; }

        public bool Equals(DtoBase other)
        {
            return Id.Equals(other.Id);
        }


    }
}
