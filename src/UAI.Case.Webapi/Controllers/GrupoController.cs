using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Domain.Academico;
using UAI.Case.Application;
using AutoMapper;
using UAI.Case.Dto;
using UAI.Case.Domain.Roles;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class GrupoController : UaiCaseController
    {
        IAlumnoAppService _alumnoAppService;
        IGrupoAppService _grupoAppService;
        ICursoAppService _cursoAppService;
        IDocenteAlumnoCursoAppService _docenteAlumnoCursoAppService;
        public GrupoController(IGrupoAppService grupoAppService, ICursoAppService cursoAppService, IAlumnoAppService alumnoAppService, IDocenteAlumnoCursoAppService docenteAlumnoCursoAppService)
        {
            _docenteAlumnoCursoAppService = docenteAlumnoCursoAppService;
            _grupoAppService = grupoAppService;
            _cursoAppService = cursoAppService;
            _alumnoAppService = alumnoAppService;
        }
        // GET: api/values
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            GrupoDTO grupo = new GrupoDTO();

            
            grupo.Grupo = Mapper.Map<Grupo>(_grupoAppService.Get(id));
            grupo.Alumnos = _docenteAlumnoCursoAppService.GetAlumnosFromGrupo(id);
            
            return Ok(grupo);
        }

      

        [HttpGet("curso/{id}")]
        public IActionResult GetByCurso(Guid id)
        {
            return Ok(Mapper.Map<IList<Grupo>>(_grupoAppService.GetAll(_ => _.Curso).Where(i => i.Curso.Id.Equals(id))));
        }

        [HttpGet("count/{id}")]
        public IActionResult GetCountAlumnosByCurso(Guid id)
        {
            return Ok(_docenteAlumnoCursoAppService.GetAlumnosFromGrupo(id).Count);
        }






        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5



     



      
            
        [HttpPut]
        public IActionResult Put([FromBody]Grupo grupo)
        {
                        
            _grupoAppService.SaveOrUpdate(grupo);

            return Ok(grupo);
            

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
