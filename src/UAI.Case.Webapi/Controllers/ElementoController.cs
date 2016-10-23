using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using UAI.Case.Domain.Proyectos;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class ElementoController : UaiCaseController
    {
        IElementoAppService _elementoAppService;
        public ElementoController(IElementoAppService elementoAppService)
        {
            _elementoAppService = elementoAppService;
        }
        // GET: api/values
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var ele = _elementoAppService.GetAll(_=>_.Elementos).Where(p=>p.Id.Equals(id)).FirstOrDefault();
            return Ok(AutoMapper.Mapper.Map<Elemento>(ele));
        }

        // GET api/values/5
        [HttpGet("proyecto/{id}")]
        public IActionResult GetByProyect(Guid id)
        {
            return Ok(_elementoAppService.GetAll(_=>_.Usuario   ).Where(x => x.IdProyecto == id));
        }

        // POST api/values
        [HttpPut]
        public IActionResult Post([FromBody]Elemento elemento)
        {
            return Ok(_elementoAppService.SaveOrUpdate(elemento));
        }

      
    }
}
