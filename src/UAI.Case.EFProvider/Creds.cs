using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Interfaces;

namespace UAI.Case.EFProvider
{
    public class Creds : ICreds
    {
        public string host { get; set; }
        public string cs { get; set; }
        public string password { get; set; }
        public string username { get; set; }

    }
}
