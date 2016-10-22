using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UAI.Case.Security
{
    public interface IAuthDataExtractor
    {
        IAuthenticatedData GetCurrentData();
    }

    public class AuthDataExtractor : IAuthDataExtractor
    {
        private readonly IHttpContextAccessor _contextAccessor;


        public AuthDataExtractor(IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
        }

        public IAuthenticatedData GetCurrentData()
        {
            if (_contextAccessor == null || (_contextAccessor != null && _contextAccessor.HttpContext != null && !_contextAccessor.HttpContext.User.Identity.IsAuthenticated))
                return null;
            else
            {
                if (_contextAccessor.HttpContext != null)
                {
                    var authData = new AuthenticatedData();
                    var identity = (ClaimsIdentity)_contextAccessor.HttpContext.User.Identity;

                    authData.UsuarioId = Guid.Parse(identity.Claims.Where(c => c.Type == "nameidentifier").Select(c => c.Value).SingleOrDefault());

                    //Guid.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault());
                    

                    return authData;
                }
                else
                    return null;

            }
        }
    }
}
