using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Roles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using UAI.Case.Dto;
using AutoMapper;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;
using UAI.Case.Webapi.hubs;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class CursoController : UaiCaseController
    {

        IDocenteAlumnoCursoAppService _docenteAlumnoCursoAppService;
        ICursoAppService _cursoAppService;
        IAlumnoAppService _alumnoAppService;
        IJoinCursoRequestAppService _joinCursoAppService;
        IHubContext _hubContext;
       
        public CursoController(ICursoAppService cursoAppService, IAlumnoAppService alumnoAppService, IConnectionManager connectionManager, IDocenteAlumnoCursoAppService docenteAlumnoCursoAppService, IJoinCursoRequestAppService joinCursoAppService)
        {
            _alumnoAppService = alumnoAppService;
            _docenteAlumnoCursoAppService = docenteAlumnoCursoAppService;
            _cursoAppService = cursoAppService;
            _joinCursoAppService = joinCursoAppService;
            _hubContext = connectionManager.GetHubContext<MessageHub>();
                   

        }


        [HttpGet("materia/{id}")]
        public IActionResult GetallCursosFromMateria(Guid id)
        {
            List<Curso> lista = _cursoAppService.GetAll(_ => _.Docente,_=>_.Materia).Where(p => p.Materia.Id.Equals(id)).ToList();

        

            return Ok(Mapper.Map<IList<CursoDTO>>(lista));
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
                return Ok(Mapper.Map<IList<Curso>>(_cursoAppService.GetAll(_ => _.Docente,_=>_.Materia)));
        }

        [HttpGet("activos")]
        public IActionResult GetActivos()
        {
            return Ok(_cursoAppService.GetAll(_ => _.Docente, _ => _.Materia).Where(p=>p.Activo));
        }




        [HttpGet("mi-grupo-curso/{id}")]
        public IActionResult MiGrupo(Guid id)
        {
            //un solo grupo por alumno por curso
            
            IList<AlumnoCursoGrupo> g = _docenteAlumnoCursoAppService.GetAll(_=>_.Grupo, _ => _.Curso).Where(a => a.Curso.Id.Equals(id) && a.Alumno.Id.Equals(UsuarioId)).ToList();

                return Ok(g);


        }

        // GET api/values/5
        [HttpGet("sin-grupo/{id}")]
        public IActionResult GetSinGrupo(Guid id)
        {
            
            return Ok(_docenteAlumnoCursoAppService.GetAlumnosSinGrupo(id));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(Mapper.Map<Curso>( _cursoAppService.Get(id, _ => _.Docente, _ => _.Materia)));
        }


    
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Curso curso)
        {
            
            return Ok(_cursoAppService.SaveOrUpdate(curso));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
