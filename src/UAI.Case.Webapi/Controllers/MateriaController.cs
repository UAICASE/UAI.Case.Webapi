using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using Microsoft.AspNetCore.Hosting;
using UAI.Case.Domain.Academico;
using Microsoft.AspNetCore.Http;
using UAI.Case.Domain.Common;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class MateriaController : UaiCaseController
    {


        
        IMateriaAppService _materiaAppService;
        IUsuarioAppService _usuarioAppService;
        IDocenteAlumnoCursoAppService _docenteAlumnoCursoAppService;
        public MateriaController(IMateriaAppService materiaAppService,  IUsuarioAppService usuarioAppService, IDocenteAlumnoCursoAppService docenteAlumnoCursoAppService)
        {
            
            _materiaAppService = materiaAppService;
            _usuarioAppService = usuarioAppService;
            _docenteAlumnoCursoAppService = docenteAlumnoCursoAppService;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
          
            return Ok(_materiaAppService.GetAll(_=>_.Titular).ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_materiaAppService.Get(id,_=>_.Titular));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]String value)
        {
        
            return Ok(_materiaAppService.GetAll().Where(m=>m.Nombre.Contains(value)).ToList());
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Materia value)
        {

            _materiaAppService.SaveOrUpdate(value);
            return Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
           
        }
    }
}
