using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Domain.Academico;
using UAI.Case.Application;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class NotaController : UaiCaseController
    {
        IDocenteAlumnoCursoAppService _alumnoCursoGrupoAppService;
        INotaAppService _notaAppService;
        public NotaController(INotaAppService notaAppService, IDocenteAlumnoCursoAppService alumnoCursoGrupoAppService)
        {
            _alumnoCursoGrupoAppService = alumnoCursoGrupoAppService;
            _notaAppService = notaAppService;
        }

        // PUT api/values/5


        [HttpGet("curso/{id}")]
        public IActionResult GetNotasFromCurso(Guid id)
        {
            IList<Nota> notas = new List<Nota>();
            List<AlumnoCursoGrupo> acg = _alumnoCursoGrupoAppService.GetAll().Where(o => o.Curso.Id.Equals(id)).ToList();
            foreach (var item in acg)
            {
                foreach (var nota in item.Notas)
                {
                    notas.Add(nota);
                }
            }

            return Ok(notas);
        }

        [HttpPut]
        public IActionResult  Put([FromBody]Nota nota)
        {
            _notaAppService.SaveOrUpdate(nota);

            return Ok(nota);
        }

       
    }
}
