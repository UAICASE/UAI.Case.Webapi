using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Exceptions.Security
{
    public class InvalidTokenRegisterException : Exception
    {
        public InvalidTokenRegisterException(String message) : base  (message)
        {

        }
    }
}
