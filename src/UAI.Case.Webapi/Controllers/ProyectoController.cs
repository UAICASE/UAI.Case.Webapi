using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using UAI.Case.Domain.Proyectos;
using UAI.Case.Dto;
using UAI.Case.Domain.Enums;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using UAI.Case.Webapi.hubs;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Roles;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class ProyectoController : UaiCaseController
    {
        IProyectoAppService _proyectoAppService;
        IUsuarioAppService _usuarioAppService;
        IHubContext _hubContext;
        ILogAppService _logAppService;
        IElementoAppService _elementoAppService;
        IDocenteAlumnoCursoAppService _docenteAlumnoCursoAppService;
        public ProyectoController(IProyectoAppService proyectoAppService,  IUsuarioAppService usuarioAppService, IConnectionManager connectionManager, ILogAppService logAppService, IElementoAppService elementoAppService, IDocenteAlumnoCursoAppService docenteAlumnoCursoAppService)
        {
            _usuarioAppService = usuarioAppService;
            _proyectoAppService= proyectoAppService;
            _hubContext = connectionManager.GetHubContext<MessageHub>();
            _logAppService = logAppService;
            _elementoAppService = elementoAppService;
            _docenteAlumnoCursoAppService = docenteAlumnoCursoAppService;
        }


        private int contar(Elemento elemento)
        {
            int c = 0;
            if (elemento.Elementos != null)
            {
                if (!elemento.IsFolder)
                { c++; }
                else
                    foreach (Elemento e in elemento.Elementos)
                    {

                        { c += contar(e); }

                    }
            }
            return c;
        }
        // GET: api/values
        [HttpGet("count-diagramas")]
        public IActionResult GetCountDiagramas()
        {
            
            IQueryable<Proyecto> lista = _proyectoAppService.GetAll().Where(d => d.Usuario.Id.Equals(UsuarioId));

            IQueryable<Elemento> list = _elementoAppService.GetAll().Where(d => d.Usuario.Id.Equals(UsuarioId));
            int c = 0;
            //foreach (Proyecto item in lista)
            //{
                foreach (Elemento e in list)
                {
                    c += contar(e);
                }


           // }


            return Ok(c);
        }


        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
          
          
            
            return Ok(Mapper.Map<List<Proyecto>,List<ProyectoDTO>>(_proyectoAppService.GetAll(_ => _.Usuario, _ => _.Curso, _=>_.Grupo).Where(p => p.Usuario.Id == UsuarioId).ToList()));
        }

        [HttpGet("grupo/{id}")]
        public IActionResult GetFromGrupo(Guid id)
        {


            return Ok(Mapper.Map<List<Proyecto>, List<ProyectoDTO>>(_proyectoAppService.GetAll(_ => _.Usuario, _ => _.Curso, _ => _.Grupo).Where(p => p.Grupo.Id == id).ToList()));
        }



        [HttpGet("curso/{id}")]
        public IActionResult GetFromCurso(Guid id)
        {


            return Ok(Mapper.Map<List<Proyecto>, List<ProyectoDTO>>(_proyectoAppService.GetAll(_ => _.Usuario, _ => _.Curso, _ => _.Grupo).Where(p => p.Curso.Id == id && p.VisibleCurso).ToList()));
        }

        [HttpGet("count")]
        public IActionResult GetCount()
        {
            

            return Ok(_proyectoAppService.GetAll().Where(p => p.Usuario.Id== UsuarioId && p.Estado.Equals(EstadoProyecto.EnProceso)).ToList().Count());
        }


        [HttpGet("resumen")]
        public IActionResult GetResumen()
        {
            

            ResumenProyectoDTO dto = new ResumenProyectoDTO();
            dto.Count = _proyectoAppService.GetAll(_ => _.Usuario).Where(p => p.Usuario.Id.Equals(UsuarioId)).ToList().Count(); //falta where en proceso
            var c = _proyectoAppService.GetAll(_ => _.Usuario).Where(p => p.Usuario.Id== UsuarioId).OrderByDescending(t => t.FechaCreacion).ToList().FirstOrDefault();
            if (c != null)
                dto.Lastest = c.FechaCreacion;
            

            return Ok(dto);
        }



        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
              return Ok( Mapper.Map<ProyectoDTO>(_proyectoAppService.Get(id,_=>_.Usuario,_=>_.Curso)));

        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }


        //solo accesible por docentes
        [HttpPut("share/grupo/")]
        public IActionResult shareGrupo([FromBody]Proyecto proyecto)
        {

            if (proyecto.Estado.Equals(EstadoProyecto.Nuevo))
                proyecto.Estado = EstadoProyecto.EnProceso;

            _proyectoAppService.SaveOrUpdate(proyecto);

            if (proyecto.Grupo != null)
            {
                Usuario usu = _usuarioAppService.Get(UsuarioId);
                LogMessage log = new LogMessage();
                LogMessage log2 = new LogMessage();
                log2.Tag = TipoTagLog.TagGrupoProyectoCompartido;
                log.Tag = Domain.Enums.TipoTagLog.TagGrupoProyectoCompartido;

                log.IdDestino = proyecto.Grupo.Id;
                log2.IdDestino = proyecto.Id;
                if (usu != null)
                {
                    log.Mensaje = usu.Nombre + " " + usu.Apellido;
                    log2.Mensaje = log.Mensaje;
                }
                
                log.Mensaje2 = "Proyecto Compartido";
                log2.Mensaje2 = "Proyecto Compartido";
                _logAppService.SaveOrUpdate(log);

                _logAppService.SaveOrUpdate(log2);

                _hubContext.Clients.Group(proyecto.Id.ToString()).newLog(log2);
                _hubContext.Clients.Group(proyecto.Grupo.Id.ToString()).newLog(log);
            }


            //return Ok(proyecto);
            return Ok(Mapper.Map<ProyectoDTO>(proyecto));

        }
        //solo accesible por alumnos
        [HttpPut("share/curso/")]
        public IActionResult sharECurso([FromBody]Proyecto proyecto)
        {

            if (proyecto.Estado.Equals(EstadoProyecto.Nuevo))
                proyecto.Estado = EstadoProyecto.EnProceso;

            _proyectoAppService.SaveOrUpdate(proyecto);

            if (proyecto.Curso != null)
            {
                Usuario usu = _usuarioAppService.Get(UsuarioId);
              LogMessage log = new LogMessage();
                LogMessage log2 = new LogMessage();
                log2.Tag = TipoTagLog.TagGrupoProyectoCompartido;
                log.Tag = Domain.Enums.TipoTagLog.TagGrupoProyectoCompartido;

                    log2.IdDestino = proyecto.Id;
                if (usu != null)
                {
                    log2.Mensaje = usu.Nombre + " " + usu.Apellido;
                    }

                  log2.Mensaje2 = "Proyecto Visible";
                _logAppService.SaveOrUpdate(log2);

                _hubContext.Clients.Group(proyecto.Id.ToString()).newLog(log2);
               // _hubContext.Clients.Group(proyecto.Grupo.Id.ToString()).newCursoLog(log);
            }


            //return Ok(proyecto);
            return Ok(Mapper.Map<ProyectoDTO>(proyecto));

        }


        //solo accesible por docentes
        [HttpGet("share/curso/all")]
        public IActionResult AllSharedWithMeFromCurso()
        {
            var px = new List<Proyecto>();
            //si soy docente, todos los proyectos de mis cursosd

            px = _proyectoAppService.GetAll().Where(p => p.Curso.Docente.Id.Equals(UsuarioId) && p.VisibleCurso).ToList();
            


            return Ok(Mapper.Map<List<ProyectoDTO>>(px));

        }
        //solo accesible por alumnos
        [HttpGet("share/grupo/all")]
        public IActionResult AllSharedWithMeFromGrupo()
        {
            var proyectos = new List<ProyectoDTO>();
            //si soy docente, todos los proyectos de mis cursosd
       //  var cursos = _docenteAlumnoCursoAppService.

            //si soy alumno
           var grupos = _docenteAlumnoCursoAppService.GetMisGruposActivos(UsuarioId);
            foreach (var item in grupos)
            {
                var px = _proyectoAppService.GetAll().Where(p => p.Grupo.Id.Equals(item.Id));
                foreach (var proyecto in px)
                {
                    if (!proyecto.Usuario.Id.Equals(UsuarioId))
                    {
                        // si no es mio, lo agrego
                        proyectos.Add(Mapper.Map<ProyectoDTO>(proyecto));
                    }
                }
            }


            return Ok(Mapper.Map<List<ProyectoDTO>>(proyectos));

        }
        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Proyecto proyecto)
        {


            


            LogMessage log = new LogMessage();
            log.Tag = Domain.Enums.TipoTagLog.TagProyecto;

            Usuario usu = _usuarioAppService.Get(UsuarioId);
            if (usu != null)
            {
                log.Mensaje = usu.Nombre + " " + usu.Apellido;
                
            }

            

            if (proyecto.Estado.Equals(EstadoProyecto.Nuevo))
            {
                proyecto.Estado = EstadoProyecto.EnProceso;

              
                
                log.Mensaje2 = "Proyecto creado";


            }
            else
            {
                if (proyecto.Estado.Equals(EstadoProyecto.EnProceso))
                {
                    log.Mensaje2 = "Proyecto editado";
                }
            }


            _proyectoAppService.SaveOrUpdate(proyecto);

             log.IdDestino = proyecto.Id;
            _logAppService.SaveOrUpdate(log);
            _hubContext.Clients.Group(proyecto.Id.ToString()).newLog(log);

            //return Ok(proyecto);
            return Ok(Mapper.Map<ProyectoDTO>(proyecto));

        }



        [HttpGet("collaborators/{id}")]
        public IActionResult Collaborators(Guid id)
        {

            IList<Usuario> collaborators = new List<Usuario>();

            Proyecto p = _proyectoAppService.Get(id);
            Usuario yo = _usuarioAppService.Get(UsuarioId);
            if (p!=null && p!=null)
            {
                //yo, por supuesto
                collaborators.Add(yo); 

                //si es viible en el curso, eldocente puede colaborar
                if (p.VisibleCurso && !yo.Id.Equals(p.Curso.Docente.Id))
                    collaborators.Add(p.Curso.Docente);


                if (p.Grupo!=null)
                {
                    //si tiene grupo asignado, es visible dsde el grupo
                    IList<Alumno> alumnos = _docenteAlumnoCursoAppService.GetAlumnosFromGrupo(p.Grupo.Id).ToList();
                    foreach (var item in alumnos)
                    {
                        if (!item.Id.Equals(UsuarioId)) //si soy yo no me vuelvo a cargar
                            collaborators.Add((Usuario)item);
                    }



                        
                }

                return Ok(Mapper.Map<IList<Usuario>>(collaborators));
                

            }


            return NotModified(id);
        }


        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
