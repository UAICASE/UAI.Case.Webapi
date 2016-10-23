using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using UAI.Case.Domain.Roles;
using UAI.Case.Domain.Enums;
using UAI.Case.Utilities;
using UAI.Case.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using UAI.Case.Exceptions.Security;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class AlumnoController : UaiCaseController
    {
        // GET: api/values
        IAlumnoAppService _alumnoAppService;
        IUsuarioAppService _usuarioAppService;
        IRequestRegisterTokenAppService _requestRegisterTokenAppService;
        public AlumnoController(IAlumnoAppService alumnoAppService, IUsuarioAppService usuarioAppService, IRequestRegisterTokenAppService requestRegisterTokenAppService)
        {
            _alumnoAppService= alumnoAppService;
            _usuarioAppService = usuarioAppService;
            _requestRegisterTokenAppService = requestRegisterTokenAppService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_alumnoAppService.GetAll());
        }

    

        // PUT api/values/5
        [HttpPut]
        
        public IActionResult Put([FromBody]Alumno alumno)
        {
            
            alumno.Rol = Rol.Alumno;
            return Ok(_alumnoAppService.SaveOrUpdate(alumno));
        }

        [HttpPut("public/{token}")]
        [AllowAnonymous]
        public IActionResult PutPublic(Guid token,[FromBody]Alumno alumno)
        {
            try
            {
                 RequestRegisterToken tk = _requestRegisterTokenAppService.Get(token);
                if (tk == null && !tk.IsValid())
                    throw new InvalidTokenRegisterException("token not valid");
                tk.Actived = true;
                if (alumno.FechaNacimiento == null)
                    alumno.FechaNacimiento = DateTime.Now;
                alumno.Rol = Rol.Alumno;
                alumno.Mail = tk.Mail;
                Usuario usuario = _alumnoAppService.SaveOrUpdate(alumno);
                _usuarioAppService.SendActivationRequest(usuario, HttpContext.Request.Host.Value,tk.Usuario.Id);
                _requestRegisterTokenAppService.SaveOrUpdate(tk);
                return Ok(usuario);
            }

            catch (InvalidTokenRegisterException e)
            {
                var a = e;
                return RegisterTokenNotValid(null);
            }
            catch (Exception e)
            {
                throw e;
                
            }
          
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
