﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Security
{
    public class InvalidActiveException : Exception
    {
        public InvalidActiveException(string message) : base(message){ }
    }
}
