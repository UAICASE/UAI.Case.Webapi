using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Security
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException(string message) : base(message){}
    }
}
