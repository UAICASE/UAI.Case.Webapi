﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Security
{
    public class InvalidActivationException : Exception
    {
        public InvalidActivationException(string message) : base(message){ }
    
    
    }
}
