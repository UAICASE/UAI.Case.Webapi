using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UAI.Case.Security
{
    public interface IAuthenticatedData
    {
        Guid UsuarioId { get; set; }
        
    }

    public class AuthenticatedData : IAuthenticatedData
{
        
       public   Guid UsuarioId { get; set; }
        
    }
}
