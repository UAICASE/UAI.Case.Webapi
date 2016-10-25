using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;



namespace UAI.Case.Security
{
    public static class Keys
    {
        private static RsaSecurityKey _rsaKey = null;
        private static RSAParameters _rsaParams;
        
        
        

        public static RsaSecurityKey RSAKey
        {
            get
            {
                if (_rsaKey == null)
                {
                    _rsaParams = GetRandomKey();
                    _rsaKey = new RsaSecurityKey(_rsaParams);

                    
                }

                return _rsaKey;
            }
        }


        private static RSAParameters GetRandomKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    return rsa.ExportParameters(true);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

    }
}
