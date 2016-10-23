using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using Microsoft.AspNetCore.Http;
using UAI.Case.Domain.Academico;

using UAI.Case.Dto;
using AutoMapper;
using UAI.Case.Domain.Roles;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;

using Microsoft.AspNetCore.SignalR.Infrastructure;
using UAI.Case.Webapi.hubs;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class DocenteAlumnoCursoController : UaiCaseController
    {
        IHubContext _hubContext;
        ILogAppService _logAppService;
        IUsuarioAppService _usuarioAppService;
        IAlumnoAppService _alumnoAppService;
        IDocenteAlumnoCursoAppService _docenteAlumnoCursoAppService;
        ICursoAppService _cursoAppService;
        IGrupoAppService _grupoAppService;
        public DocenteAlumnoCursoController(IAlumnoAppService alumnoAppService,ILogAppService logAppService,  IConnectionManager connectionManager,IDocenteAlumnoCursoAppService docenteAlumnoCursoAppService,ICursoAppService cursoAppService, IUsuarioAppService usuarioAppService, IGrupoAppService grupoAppService)
        {
            _docenteAlumnoCursoAppService = docenteAlumnoCursoAppService;
            _cursoAppService = cursoAppService;
              _usuarioAppService = usuarioAppService;
            _grupoAppService = grupoAppService;
            _hubContext = connectionManager.GetHubContext<MessageHub>();
            _logAppService = logAppService;
            _alumnoAppService = alumnoAppService;
        }


        [HttpGet("mis-grupos-activos")]
        public IActionResult GetMisGrupos()
        {

            

            Usuario a = _usuarioAppService.Get(UsuarioId);

           IList<Grupo> grupos = new List<Grupo>();
            //Grupo dto;

            
            if (a != null && a.Rol.Equals(Rol.Alumno))
            {

               grupos = _docenteAlumnoCursoAppService.GetMisGruposActivos(a.Id);


            }
        
            

            return Ok(grupos);


        }


        [HttpPut("solicitar-acceso/curso/{estado}")]
        public IActionResult OkSolicitarAcceso([FromBody]AlumnoCursoGrupo value,EstadoAfectacion estado)
        {
            value.Estado = estado;
            _docenteAlumnoCursoAppService.SaveOrUpdate(value);


            LogMessage log = new LogMessage();
            log.IdDestino = value.Curso.Id; //los del dashboard
            log.Tag = TipoTagLog.TagRequestJoinGrupoAccepted;
            log.Mensaje = "Solicitud de usuario ";
            log.Mensaje2 = "el alumno " + value.Alumno.Apellido + " " + value.Alumno.Nombre + " se unio al curso"; //TODO: ver mensaje
            _logAppService.SaveOrUpdate(log);

            _hubContext.Clients.Group(value.Curso.Id.ToString()).newLog(log);

            if (value.Estado == EstadoAfectacion.Aceptada)
            {
                LogMessage log2 = new LogMessage();
                log2.IdDestino = value.Alumno.Id;
                log2.Tag = TipoTagLog.TagRequestJoinGrupoAccepted;
                log2.Mensaje = "Solicitud aceptada ";
                log2.Mensaje2 = "te uniste al curso"; //TODO: ver mensaje
                _logAppService.SaveOrUpdate(log2);



                String[] connections = MessageHub.GetConnectionMapping().GetConnections(value.Alumno.Id.ToString()).ToArray();
                foreach (string item in connections)
                {
                    _hubContext.Clients.Client(item).newLog(log);
                    if (value.Estado == EstadoAfectacion.Aceptada)
                        _hubContext.Clients.Client(item).newCursoJoinRequestAccepted(value);
                }
            }

            return Ok(value);
        }

        [HttpPut("solicitar-acceso/curso")]
        public IActionResult SolicitarAcceso([FromBody]AlumnoCursoGrupo value)
        {

            Alumno alumno = _alumnoAppService.Get(UsuarioId);
            if (alumno != null)
            {
                AlumnoCursoGrupo tmp = _docenteAlumnoCursoAppService.GetAll().Where(x => x.Curso.Id.Equals(value.Curso.Id) && x.Alumno.Id.Equals(alumno.Id)).FirstOrDefault();

                if (tmp == null)
                {
                    value.Estado = EstadoAfectacion.Pendiente;
                    value.Alumno = alumno;
                    _docenteAlumnoCursoAppService.SaveOrUpdate(value);
                    LogMessage log = new LogMessage();
                    log.IdDestino = value.Curso.Docente.Id; //los del dashboard
                    log.Tag = TipoTagLog.TagRequestJoinGrupo;
                    log.Mensaje = "Solicitud de usuario ";
                    log.Mensaje2 = "el alumno " + value.Alumno.Apellido + " " + value.Alumno.Nombre + " desea unirse un curso"; //TODO: ver mensaje
                    _logAppService.SaveOrUpdate(log);

                    _hubContext.Clients.Group(value.Curso.Id.ToString()).newCursoJoinRequest(value);

                    String[] connections = MessageHub.GetConnectionMapping().GetConnections(value.Curso.Docente.Id.ToString()).ToArray();
                    foreach (string item in connections)
                    {
                        _hubContext.Clients.Client(item).newLog(log);
                    }


                    return Ok(value);
                }
                else
                {
                    if (tmp.Estado == EstadoAfectacion.Pendiente)
                        return NotModified(value);
                    else
                        return InvalidUser(value);

                }
            }

            else
                return InvalidUser(value);
        
    }

        [HttpGet("solicitudes-pendientes/curso/{id}")]
        public IActionResult GetSolicitudesPendientes(Guid id)
        {
            

            IList<AlumnoCursoGrupo> pendientes = _docenteAlumnoCursoAppService
                 .GetAll(_ => _.Curso, _ => _.Alumno, _ => _.Grupo).Where(x => x.Curso.Id.Equals(id) && x.Estado == EstadoAfectacion.Pendiente).ToList();

            

            return Ok(Mapper.Map<IList<AlumnoCursoGrupo>>(pendientes));
        }

        [HttpGet("mis-materias")]
        public IActionResult GetMisMaterias()
        {
            IList<Materia> misMaterias = new List<Materia>();

            //traigo las materias que corresponden a los cursos que dicta (pueden ser otras)
            var lista = _docenteAlumnoCursoAppService.GetAll().Where(o => o.Curso.Docente.Id.Equals(UsuarioId) || o.Curso.Materia.Titular.Id.Equals(UsuarioId)).ToList();

            foreach (var item in lista)
            {
                if (!misMaterias.Contains(item.Curso.Materia))
                    misMaterias.Add(item.Curso.Materia);
            }

            return Ok(Mapper.Map<IList<Materia>>(misMaterias));
        }




        [HttpGet("mis-cursos")]
            public IActionResult GetMisCursos()
        {

            
             Usuario a = _usuarioAppService.Get(UsuarioId);
             
            IList<CursoDTO> misCursos = new List<CursoDTO>();
            CursoDTO dto;
            IList<CursoDTO> cursos = Mapper.Map<IList<CursoDTO>>( _cursoAppService.GetAll().ToList());



            if (a != null && a.Rol.Equals(Rol.Alumno))
            {

                IList<AlumnoCursoGrupo> lista = Mapper.Map<List<AlumnoCursoGrupo>>( _docenteAlumnoCursoAppService
                   .GetAll(_ => _.Curso, _ => _.Alumno, _ => _.Grupo)
                   .Where(c => c.Alumno.Id.Equals(UsuarioId) && c.Estado==EstadoAfectacion.Aceptada).ToList());

                
                foreach (var curso in cursos)
                {
                    


                    foreach (var item in lista)
                    {
                        dto = Mapper.Map<CursoDTO>(item.Curso);
                        if (curso.Id.Equals(item.Curso.Id))
                        {
                            curso.Grupo = item.Grupo;
                            curso.Mio = item.Alumno.Id.Equals(UsuarioId);
                            //misCursos.Add(dto);
                        }
                    }

                 
                }
            }
            else
             if (a != null && a.Rol.Equals(Rol.Docente))
            {
                //IList<CursoDTO> lista =   cursos.AsQueryable().Where(x => x.Docente.Id.Equals(UsuarioId)).ToList();  //_cursoAppService.GetAll().Where(x => x.Docente.Id.Equals(UsuarioId)).ToList();
                foreach (var item in cursos)
                {
                    item.Mio = (item.Docente.Id.Equals(UsuarioId));


                }
            }





            //TODO: Crear uyn mapper de AlumnoCurso a CursoDTO y otro a AlumnoDTO

            return Ok(Mapper.Map<List<CursoDTO>>(cursos));


        }

        // GET: api/values
        [HttpGet("mis-cursos-activos")]
        public IActionResult GetMisCursosActivos()
        {

            

            Usuario a = _usuarioAppService.Get(UsuarioId);
             
            IList<CursoDTO> misCursos = new List<CursoDTO>();
            CursoDTO dto;


            if (a != null && a.Rol.Equals(Rol.Alumno))
            {
                IList<AlumnoCursoGrupo> lista = _docenteAlumnoCursoAppService
                   .GetAll(_ => _.Curso, _ => _.Alumno)
                   .Where(c => c.Alumno.Id.Equals(UsuarioId) && c.Curso.Activo && c.Estado==EstadoAfectacion.Aceptada).ToList();
                foreach (AlumnoCursoGrupo item in lista)
                {
                    dto = Mapper.Map<CursoDTO>(item.Curso);
                    if (!misCursos.Contains(dto))
                        misCursos.Add(dto);
                }
            }
            else
             if (a != null && a.Rol.Equals(Rol.Docente))
            {
                IList<Curso> lista = _cursoAppService
                   .GetAll()
                   .Where(c => c.Docente.Id.Equals(UsuarioId) && c.Activo).ToList();
                foreach (Curso item in lista)
                {
                    dto = Mapper.Map<CursoDTO>(item);
                    if (!misCursos.Contains(dto))
                        misCursos.Add(dto);
                }
            }

            
            
        

            //TODO: Crear uyn mapper de AlumnoCurso a CursoDTO y otro a AlumnoDTO

            return Ok(misCursos);


         
        }

        [HttpGet("mis-cursos-finalizados")]
        public IActionResult GetMisCursosFinalizados()
        {


            Usuario a = _usuarioAppService.Get(UsuarioId);
             
            IList<CursoDTO> misCursos = new List<CursoDTO>();
            CursoDTO dto;


            if (a != null && a.Rol.Equals(Rol.Alumno))
            {
                IList<AlumnoCursoGrupo> lista = _docenteAlumnoCursoAppService
                   .GetAll(_ => _.Curso, _ => _.Alumno)
                   .Where(c => c.Alumno.Id.Equals(UsuarioId) && !c.Curso.Activo && c.Estado == EstadoAfectacion.Aceptada).ToList();
                foreach (AlumnoCursoGrupo item in lista)
                {
                        dto = Mapper.Map<CursoDTO>(item.Curso);
                    if (!misCursos.Contains(dto))
                        misCursos.Add(dto);
                }
            }
            else
             if (a != null && a.Rol.Equals(Rol.Docente))
            {
                IList<Curso> lista = _cursoAppService
                   .GetAll()
                   .Where(c => c.Docente.Id.Equals(UsuarioId) && !c.Activo).ToList();
                foreach (Curso item in lista)
                {
                    dto = Mapper.Map<CursoDTO>(item);
                    if (!misCursos.Contains(dto))
                        misCursos.Add(dto);
                }
            }





            //TODO: Crear uyn mapper de AlumnoCurso a CursoDTO y otro a AlumnoDTO

            return Ok(misCursos);

        }

                
        [HttpGet("curso/{id}")]
        public IActionResult GetAlumnos(Guid id)
        {
            var lista = _docenteAlumnoCursoAppService.GetAll(_ => _.Curso, _ => _.Alumno, _ => _.Grupo).Where(p => p.Curso.Id.Equals(id)).ToList();
            return Ok(Mapper.Map<List<AlumnoCursoGrupo>>(lista));
        }



        [HttpGet("curso/alumnos/{id}")]
        public IActionResult GetAlumnoByCursos(Guid id)
        {
            List<AlumnoCursoGrupo> lista=_docenteAlumnoCursoAppService.GetAll(_ => _.Curso, _ => _.Alumno, _ => _.Grupo).Where(p => p.Curso.Id.Equals(id) && p.Estado==EstadoAfectacion.Aceptada).ToList();

            return Ok(Mapper.Map<List<AlumnoCursoGrupo>>(lista));
        }

        
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put( [FromBody]AlumnoCursoGrupo value)
        {
                       
            _docenteAlumnoCursoAppService.SaveOrUpdate(value);

            //if (value.Estado == EstadoAfectacion.Aceptada)
            //{
            //    LogMessage log2 = new LogMessage();
            //    log2.IdDestino = value.Alumno.Id;
            //    log2.Tag = TipoTagLog.TagRequestJoinGrupoAccepted;
            //    log2.Mensaje = "Bienvenido ";
            //    log2.Mensaje2 = "te uniste al curso"; //TODO: ver mensaje
            //    _logAppService.SaveOrUpdate(log2);



            //    String[] connections = MessageHub.GetConnectionMapping().GetConnections(value.Alumno.Id.ToString()).ToArray();
            //    foreach (string item in connections)
            //    {
            //        _hubContext.Clients.Client(item).newUserLog(log2);

            //        _hubContext.Clients.Client(item).newCursoJoinedExternal(value);

            //    }
            //}

            return Ok(value);
        }



        [HttpPut("quitar-mi-grupo")]
        public IActionResult RemoveFromGrupo([FromBody]Grupo grupo)
        {
            

            AlumnoCursoGrupo acg = _docenteAlumnoCursoAppService.GetAll().Where(p => p.Alumno.Id.Equals(UsuarioId) && p.Curso.Id.Equals(grupo.Curso.Id)).FirstOrDefault();
            if (acg != null)
            {
                acg.Grupo = null;
                _docenteAlumnoCursoAppService.SaveOrUpdate(acg);
            }
            return Ok(Mapper.Map<AlumnoCursoGrupo>(acg));
        }


        [HttpPut("mi-grupo")]
        public IActionResult PutInGrupo([FromBody]Grupo grupo)
        {
            AlumnoCursoGrupo acg = _docenteAlumnoCursoAppService.GetAll().Where(p => p.Alumno.Id.Equals(UsuarioId) && p.Curso.Id.Equals(grupo.Curso.Id)).FirstOrDefault();
            int cant = _docenteAlumnoCursoAppService.GetAlumnosFromGrupo(grupo.Id).Count;

            var gru=_grupoAppService.Get(grupo.Id);//actualizamos grupo

            if (acg!=null)
            {
                if (cant+ 1 <= gru.Maximo)
                {
                    acg.Grupo = gru;
                    _docenteAlumnoCursoAppService.SaveOrUpdate(acg);
                    return Ok(Mapper.Map<AlumnoCursoGrupo>(acg));
                }
                return UaiCaseResult(null,304);
            }
            return UaiCaseResult(null, 304);
        }

        [HttpPost("mi-grupo")]
        public IActionResult GetMyGrupo([FromBody]Curso curso)
        {
            

            Grupo grupo = _docenteAlumnoCursoAppService.GetAll().Where(p => p.Alumno.Id.Equals(UsuarioId) && p.Curso.Id.Equals(curso.Id)).FirstOrDefault().Grupo;            
            return Ok(grupo);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("alumno/{id}")]
        public IActionResult GetAllAlumnoGrupo(Guid id)
        {



            return Ok(Mapper.Map<IList<AlumnoCursoGrupo>>(_docenteAlumnoCursoAppService.GetAll().Where(a => a.Curso.Id.Equals(id)).ToList()));

        }
    }


}
