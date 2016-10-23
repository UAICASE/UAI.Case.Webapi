using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using UAI.Case.Security;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;


namespace UAI.Case.Webapi.Config
{
  
    [Authorize("Bearer")]
    public  class UaiCaseController : Controller
    {

        Guid _usuarioId;
        public Guid UsuarioId
        {
            get
            {
                if (HttpContext != null)
                {
                    var identity = (ClaimsIdentity)HttpContext.User.Identity;
                    return _usuarioId = Guid.Parse(identity.Claims.Where(c => c.Type == "nameidentifier").Select(c => c.Value).SingleOrDefault());
                }

                return Guid.Empty;
            }
        }

     public UaiCaseController()
        {
           
        }

        public IActionResult Unauthorized(object data)
        {
            HttpContext.Response.StatusCode = 401;
            return new ObjectResult(data);
        }

        public IActionResult NotFoundResult(object data)
        {
            HttpContext.Response.StatusCode = 404;
            return new ObjectResult(data);
        }
        public IActionResult InternalServerError(object data)
        {
            HttpContext.Response.StatusCode = 500;
            return new ObjectResult(data);
        }

        public IActionResult RegisterTokenNotValid(object data)
        {
            HttpContext.Response.StatusCode = 406;
            return new ObjectResult(data);
        }
        public IActionResult InvalidUser(object data)
        {
            HttpContext.Response.StatusCode = 402;
            return new ObjectResult(data);
        }

        

        public IActionResult Forbidden(object data)
        {
            HttpContext.Response.StatusCode = 403;
            return new ObjectResult(data);
        }

        public new  IActionResult Ok(object data)
        {
            HttpContext.Response.StatusCode = 200;
            return new ObjectResult(data);
        }

        public  IActionResult UaiCaseResult(object data, int statusCode)
        {
            HttpContext.Response.StatusCode = statusCode;
            return new ObjectResult(data);
        }
        public IActionResult NotModified(object data)
        {
            HttpContext.Response.StatusCode = 304;
            return new ObjectResult(data);
        }


    }
}
