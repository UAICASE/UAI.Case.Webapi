using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Security
{
    public class InvalidTokenException : Exception 
    {
        public InvalidTokenException(String message) : base  (message)
        {

        }

        
    }
}
