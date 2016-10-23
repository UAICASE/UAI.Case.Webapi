using System.Collections.Generic;
using UAI.Case.Application;
using UAI.Case.Dto;
using UAI.Case.Domain;
using UAI.Case.Security;
using UAI.Case.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using UAI.Case.Webapi.Config;

using System.Linq;
using System;

using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;
using UAI.Case.Domain.Roles;
using UAI.Case.Exceptions.Security;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Proyectos;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using UAI.Case.Webapi.hubs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Authentication;

//using Microsoft.EntityFrameworkCore;

namespace UAI.Case.Webapi.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : UaiCaseController
    {
        IUsuarioAppService _usuarioAppService;
        IAdminAppService _adminAppService;
        IAlumnoAppService _alumnoAppService;
        IDocenteAppService _docenteAppService;
        IMateriaAppService _materiappService;
        ICursoAppService _cursoAppService;
        IHubContext _hubContext;
        ILogAppService _logAppService;
        IDocenteAlumnoCursoAppService _alumnoCursoGrupoAppService;

        public AuthController(IDocenteAlumnoCursoAppService alumnoCursoGrupoAppService, IUsuarioAppService usuarioAppService, IAdminAppService adminAppService, IAlumnoAppService alumnoAppService, IDocenteAppService docenteAppService, IMateriaAppService materiaAppService, ICursoAppService cursoAppService, IConnectionManager connectionManager, ILogAppService logAppService)
        {
 
            _logAppService = logAppService;
            _usuarioAppService = usuarioAppService;
            _adminAppService = adminAppService;
            _alumnoAppService = alumnoAppService;
            _docenteAppService = docenteAppService;
            _materiappService = materiaAppService;
            _cursoAppService = cursoAppService;
            _alumnoCursoGrupoAppService = alumnoCursoGrupoAppService;
            _hubContext = connectionManager.GetHubContext<MessageHub>();
        }



        [HttpGet("initialize-data")]
        [AllowAnonymous]
        public IActionResult InitializeData()
        {
            //UaiCaseContext c = (UaiCaseContext)_db;

            
            //c.Database.EnsureDeleted();
            //c.Database.EnsureCreated();
            //c.Database.Migrate();

            CreateDefaults();
            return Ok();
        }
        private void CreateDefaults()
        {

            Usuario usuarioAdmin = _usuarioAppService.GetAll().Where(p => p.Mail == "admin@case.uai.edu.ar").ToList().FirstOrDefault();
            if (usuarioAdmin != null)
            {
                //do something
            }
            else
            {

                //ver el tema de la sesion



                Admin admin = new Admin();

                //admin.Username = "admin";
                admin.Password = "admin";
                admin.Active = true;
                admin.Perfil = "Administrador del sistema"; //TODO: completar la edicion del perfil con este campo 9-4-15
                admin.Nombre = "admin";
                admin.Rol = Rol.Admin;
                admin.Apellido = "admin";
                admin.Mail = "admin@case.uai.edu.ar";
                admin.FechaNacimiento = DateTime.Now;
                //usuario.Persona = admin;
                admin.FechaCreacion = DateTime.Now;
                //_alumnoAppService.SaveOrUpdate(admin);
                _adminAppService.SaveOrUpdate(admin);


            }


            Alumno alu1 = new Alumno();
            Alumno alu2 = new Alumno();

            Usuario usuarioAlu = _usuarioAppService.GetAll().Where(p => p.Mail == "alumno@case.uai.edu.ar").ToList().FirstOrDefault();
            if (usuarioAlu != null)
            {
                //do something
            }
            else
            {

                //ver el tema de la sesion



                 alu1 = new Alumno();

                
                alu1.Password = "alumno";
                alu1.Active = true;
                alu1.Perfil = "Alumno de prueba"; //TODO: completar la edicion del perfil con este campo 9-4-15
                alu1.Nombre = "alumno";
                alu1.Rol = Rol.Alumno;
                alu1.Apellido = "alumno";
                alu1.Mail = "alumno@case.uai.edu.ar";
                alu1.FechaNacimiento = DateTime.Now;
                
                alu1.FechaCreacion = DateTime.Now;
                _alumnoAppService.SaveOrUpdate(alu1);


                alu2 = new Alumno();
                alu2.Password = "alumno2";
                alu2.Active = true;
                alu2.Perfil = "Alumno2 de prueba"; //TODO: completar la edicion del perfil con este campo 9-4-15
                alu2.Nombre = "alumno2";
                alu2.Rol = Rol.Alumno;
                alu2.Apellido = "alumno2";
                alu2.Mail = "alumno2@case.uai.edu.ar";
                alu2.FechaNacimiento = DateTime.Now;
                alu2.FechaCreacion = DateTime.Now;
                _alumnoAppService.SaveOrUpdate(alu2);
                



            }


            Usuario usuarioDoc = _docenteAppService.GetAll().Where(p => p.Mail == "docente@case.uai.edu.ar").ToList().FirstOrDefault();
            Docente docente;
            Docente titular;
            if (usuarioDoc != null)
            {
                //do something
            }
            else
            {

                //ver el tema de la sesion


                titular = new Docente();

                //admin.Username = "admin";
                titular.Password = "docente";
                titular.Active = true;
                titular.Perfil = "Docente titular de prueba"; //TODO: completar la edicion del perfil con este campo 9-4-15
                titular.Nombre = "docente";
                titular.Rol = Rol.Docente;
                titular.Apellido = "titular";
                titular.Mail = "docentet@case.uai.edu.ar";
                titular.FechaNacimiento = DateTime.Now;
                //usuario.Persona = admin;
                titular.FechaCreacion = DateTime.Now;
                _docenteAppService.SaveOrUpdate(titular);

                docente = new Docente();

                //admin.Username = "admin";
                docente.Password = "docente";
                docente.Active = true;
                docente.Perfil = "Docente de prueba"; //TODO: completar la edicion del perfil con este campo 9-4-15
                docente.Nombre = "docente";
                docente.Rol = Rol.Docente;
                docente.Apellido = "docente";
                docente.Mail = "docente@case.uai.edu.ar";
                docente.FechaNacimiento = DateTime.Now;
                //usuario.Persona = admin;
                docente.FechaCreacion = DateTime.Now;
                _docenteAppService.SaveOrUpdate(docente);


                //TipoDiagramaMateria tipo;

                Materia materia= new Materia();
                    materia.Nombre = "MATERIA DE PRUEBA";
                    materia.Titular = titular;
                materia.Descripcion = "materia de prueba";
                materia.Codigo = "9";
                materia.Anio = 3;
                materia.DiagramasValidos.Add (new TipoDiagramaMateria(TipoDiagrama.DiagramaCasoUso));
                materia.DiagramasValidos.Add(new TipoDiagramaMateria(TipoDiagrama.DiagramaDerConceptual ));

                _materiappService.SaveOrUpdate(materia);

                Curso curso = new Curso();
                curso.Activo = true;
                curso.Anio = 2016;
                curso.Dia = Dia.Lunes;
                curso.Docente = docente;
                curso.Materia = materia;
                curso.Sede = Sede.Castelar;
                curso.Turno = Turno.Mañana;
                curso.TipoVisibilidad = TipoVisibilidadCurso.Privado;
                _cursoAppService.SaveOrUpdate(curso);



                AlumnoCursoGrupo acg = new AlumnoCursoGrupo();
                acg.Alumno = alu1;
                acg.Curso = curso;
                acg.Estado = EstadoAfectacion.Aceptada;
                _alumnoCursoGrupoAppService.SaveOrUpdate(acg);


                AlumnoCursoGrupo acg2 = new AlumnoCursoGrupo();
                acg2.Alumno = alu2;
                acg2.Curso = curso;
                acg2.Estado = EstadoAfectacion.Aceptada;
                _alumnoCursoGrupoAppService.SaveOrUpdate(acg2);



            }

        }

      

        
        [HttpPost("activate")]
        [AllowAnonymous]
        public IActionResult Activate([FromBody]ActivateAccountDTO dto)
        {

            try
            {
                Usuario u = _usuarioAppService.Get(dto.Id);
                if (u != null )
                {

                    if (!u.Active)
                    {
                        RegisterToken rt = RegisterHandler.RestoreToken(u.RegisterToken);
                        rt.IsValid();
                        if (UAI.Case.Security.Cryptography.MD5Hash(u.RegisterToken).Equals(dto.Token))
                        {
                            u.Active = true;
                            u.RegisterToken = null;
                            _usuarioAppService.SaveOrUpdate(u);



                            //aviso a quien hizo la solicitud que se activo la cuenta de este usuario
                            LogMessage log = new LogMessage();
                            log.IdDestino = u.IdRegisterRequestUserId; //los del dashboard
                            log.Tag = TipoTagLog.TagUsuarioActivado;
                            log.Mensaje = "Activación de cuenta";
                            log.Mensaje2 = "el alumno " + u.Apellido + " " + u.Nombre + " activo su cuenta";
                            _logAppService.SaveOrUpdate(log);

                          

                            String[] connections = MessageHub.GetConnectionMapping().GetConnections(u.IdRegisterRequestUserId.ToString()).ToArray();
                            foreach (string item in connections)
                            {

                                _hubContext.Clients.Client(item).newLog(log);
                            }


                            return Ok(u);
                        }
                        throw new InvalidActivationException("error de activación");
                    }
                    else
                    {
                        throw new UserActivedException("Usuario ya activo");
                    }

                }
                else {
                    throw new InvalidActivationException("error de activación");
                }
            
                

                
                    

                
            }
            catch (UserActivedException e)
            {

                HttpContext.Response.StatusCode = 429;
                return new ObjectResult(e.Message);
            }
            catch (Exception e)
            {
                return RegisterTokenNotValid(e.Message);

            }
            
        }

     

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Token([FromBody]LoginDTO login)
        {
            string token;
            object response;

        //    CreateDefaults();
            
            

            try
            {
                Usuario u = _usuarioAppService.GetByUsernamePassword(login.Mail, login.Password);//   GetAll().Where(p => p.Mail.Equals(login.Mail) && p.Password.Equals(Security.Cryptography.MD5Hash(login.Password))).FirstOrDefault();
                if (u == null)
                    throw new InvalidCredentialException();

                if (!u.Active)
                    throw new InvalidActiveException("");


                token = TokenHandler.GenerateToken(u);
                response = new { authenticated = true, token = token, id=u.Id.ToString() };
            }
            catch (InvalidCredentialException)
            {
                response = new { authenticated = false };
                return base.Unauthorized(response);

            }

            catch (InvalidActiveException)
            {
                response = new { authenticated = false };
                return base.InvalidUser(response);

            }

            return Ok(response);
        }








        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]LoginDTO login)
        {
        

            Usuario n = new Usuario();
            n.Mail = login.Mail;
            n.Password = login.Password;
            _usuarioAppService.SaveOrUpdate(n);
            return Ok(n);
        }

      
    }
}
