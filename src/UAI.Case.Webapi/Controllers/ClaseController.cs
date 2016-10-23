using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class ClaseController : UaiCaseController
    {
        IUnidadClaseAppService _unidadClaseAppService;
        IClaseAppService _claseAppService;
        public ClaseController(IClaseAppService claseAppService, IUnidadClaseAppService unidadClaseAppService)
        {
            _claseAppService = claseAppService;
            _unidadClaseAppService = unidadClaseAppService;
        }
        
        [HttpGet("curso/{id}")]
        public IActionResult GetAllFromCurso(Guid id)
        {
            IList<Clase> lista =_claseAppService.GetAll(_=>_.Curso,_=>_.Unidades).Where(o => o.Curso.Id.Equals(id)).ToList();



            return Ok(AutoMapper.Mapper.Map<IList<Clase>>(lista));

        }



        [HttpPut("unidad")]
        public IActionResult PutUnidadInClase([FromBody]UnidadClase unidadClase)
        {
            _unidadClaseAppService.SaveOrUpdate(unidadClase);
            return Ok(unidadClase);
        }

        [HttpPut]
        public IActionResult Put( [FromBody]Clase clase)
        {
            _claseAppService.SaveOrUpdate(clase);
            return Ok(clase);
        }

     
    }
}
