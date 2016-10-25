using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Domain.Interfaces
{
  public  interface ICreds
    {
      string username { get; set; }
      string password { get; set; }
      string host { get; set; }
      string uri { get; set; }
      string cs { get; set; }
      string port { get; set; }
      string  database { get; set; }

      void GetDataFromUri(string uri);

    }
}
