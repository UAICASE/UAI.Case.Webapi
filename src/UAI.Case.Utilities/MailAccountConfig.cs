using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Utilities
{
    public interface IMailAccount
    {
        string account { get; set; }
        int port { get; set; }
        string smtp { get; set; }
        string password { get; set; }

    }

    public interface IRegistrosMailAccount : IMailAccount
     {

     }


    public class MailAccountConfig : IMailAccount, IRegistrosMailAccount
    {
       public string account { get; set; }
       public int port { get; set; }
       public string smtp { get; set; }
       public string password { get; set; }

      
    }
}
