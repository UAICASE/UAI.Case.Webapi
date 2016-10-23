using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;
using UAI.Case.Exceptions;
using UAI.Case.Exceptions.Security;

namespace UAI.Case.Security
{
    public static class RegisterHandler
    {
       
        public static String GenerateRegisterToken(Guid idUsuario)
        {
            RegisterToken r = new RegisterToken();
            r.FechaVencimiento = DateTime.Now.AddHours(1);
            r.IdUsuario = idUsuario;
            return Cryptography.EncryptObject(r);
        }
        public static RegisterToken RestoreToken(string token)
        {
            try
            {
                return Cryptography.DecryptObject<RegisterToken>(token);
            }
            catch (InvalidTokenException)
            {

                throw new InvalidTokenRegisterException("token invalido");
            }
            
        }

        public static void ValidateToken(string token)
        {
            try
            {
                RegisterToken t = Cryptography.DecryptObject<RegisterToken>(token);
                if (!t.IsValid())
                    throw new Exception();
            }
            catch (Exception)
            {

                throw new InvalidTokenRegisterException("token de registro invalido");
            }
        }


    }
}
