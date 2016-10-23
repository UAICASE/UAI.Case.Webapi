
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

using UAI.Case.Application;
using UAI.Case.Domain;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;
using UAI.Case.Domain.Roles;
using UAI.Case.Dto;
using UAI.Case.Repositories;
using UAI.Case.Security;
using UAI.Case.Utilities;
using UAI.Case.Webapi.Config;

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class UsuariosController : UaiCaseController
    {

        private IUsuarioAppService _usuarioAppService;
        private IAlumnoAppService _alumnoAppService;
        IRequestRegisterTokenAppService _requestRegisterTokenAppService;
        public UsuariosController(IUsuarioAppService usuarioAppService, IRequestRegisterTokenAppService requestRegisterTokenAppService, IAlumnoAppService alumnoAppService)
        {
            _alumnoAppService = alumnoAppService;
            _requestRegisterTokenAppService = requestRegisterTokenAppService;

            this._usuarioAppService = usuarioAppService;

        }


        [HttpPost("request-register")]
        [AllowAnonymous]
        public IActionResult GetRegisterToken([FromBody]LoginDTO register)
        {


            var currentUrl = HttpContext.Request.Host.Value;
            RequestRegisterToken tk = new RequestRegisterToken();
            tk.Mail = register.Mail;
            
            _requestRegisterTokenAppService.SaveOrUpdate(tk);
            Mailer.SendRegistrationRequest(currentUrl, tk.Id.ToString(), register.Mail);
            return Ok(null);

        }

        [HttpGet("validate/{token}")]
        [AllowAnonymous]
        public IActionResult ValidateRegisterToken(Guid token)
        {
            try
            {
                RequestRegisterToken tk = _requestRegisterTokenAppService.Get(token);
                if (tk == null || !tk.IsValid())
                    throw new Exception("token not valid");

                return Ok(null);
            }
            catch (Exception)
            {

                return RegisterTokenNotValid(null);
            }


        }

       


        [HttpGet("activate/{id}")]
        [AllowAnonymous]
        public IActionResult RequestActivateUser(Guid id)
        {
            Usuario u = _usuarioAppService.Get(id);
            _usuarioAppService.SendActivationRequest(u, HttpContext.Request.Host.Value,u.IdRegisterRequestUserId);

            return Ok(u);
        }


        
        [HttpPost("password")]
        public IActionResult ChangePassword([FromBody]ChangePasswordDTO dto)
        {
            if (dto.NewPassword.Equals(dto.OldPassword))
            {
                return Unauthorized("Nuevo password no puede ser igual al anterior!");
            }
            else
            {
                
                Usuario usu = _usuarioAppService.GetAll().Where(u => u.Id.Equals(UsuarioId) && u.Password.Equals(Security.Cryptography.MD5Hash(dto.OldPassword))).FirstOrDefault();
                if (usu != null)
                {
                    if (dto.NewPassword.Equals(dto.ValidatePassword))
                    {
                        usu.Password = Security.Cryptography.MD5Hash(dto.NewPassword);
                        _usuarioAppService.SaveOrUpdate(usu);
                        return Ok(null);
                    }
                    else
                        return Unauthorized("Los Password no coinciden!");
                }
                else
                {
                    return InvalidUser("Password invalido!");
                }
            }
            
        }

        [AllowAnonymous]
        [HttpPost("check")]
        public IActionResult CheckMail([FromBody]LoginDTO mail)
        {
                 return Ok(!_usuarioAppService.GetAll().Any(u => u.Mail.Equals(mail.Mail)));
         

        }



        [HttpGet("{id}")]

        public IActionResult Get(Guid id)
        {

            return Ok(_usuarioAppService.Get(id));

        }

        [HttpPut]
        public IActionResult Put([FromBody]Usuario usuario)
        {

            return Ok(_usuarioAppService.SaveOrUpdate(usuario));

        }


    }
}
