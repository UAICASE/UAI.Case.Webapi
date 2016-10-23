using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using UAI.Case.Domain.Roles;
using UAI.Case.Domain.Enums;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class DocenteController : UaiCaseController
    {
        IDocenteAppService _docenteAppService;

        public DocenteController(IDocenteAppService docenteAppService)
        {
            _docenteAppService = docenteAppService;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_docenteAppService.GetAll());
        }
        [HttpGet("where/{value}")]
        public IActionResult Get(string value)
        {
            return Ok(_docenteAppService.GetAll().Where(d=>d.Nombre.Contains(value) || d.Apellido.Contains(value)));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Docente docente)
        {
            docente.Rol = Rol.Docente; 
            return Ok(_docenteAppService.SaveOrUpdate(docente));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
