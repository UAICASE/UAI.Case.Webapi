using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Domain.Academico;
using UAI.Case.Application;
using System.Collections;

using AutoMapper;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class ContenidoMateriaController : UaiCaseController
    {
        IContenidoAppService _contenidoAppService;
           IContenidoMateriaAppService _contenidoMateriaAppService;
        IUnidadAppService _unidadAppService;
        IArchivoAppService _archivoAppService;
        public ContenidoMateriaController(IContenidoAppService contenidoAppService, IContenidoMateriaAppService contenidoMateriaAppService, IUnidadAppService unidadAppService, IArchivoAppService archivoAppService)
        {
            _contenidoAppService = contenidoAppService;
            _contenidoMateriaAppService = contenidoMateriaAppService;
            _unidadAppService = unidadAppService;
            _archivoAppService = archivoAppService;
        }



        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {

            IList lista = _contenidoMateriaAppService.GetAll(_=>_.Materia, _=>_.Unidad).Where(o=>o.Materia.Id.Equals(id)).ToList();
            return Ok(Mapper.Map<IList<ContenidoMateria>>(lista));
        }



        [HttpPut("contenido")]
        public IActionResult PutContenido([FromBody] Contenido contenido)
        {
            _contenidoAppService.SaveOrUpdate(contenido);
            return Ok(contenido);
        }

        [HttpPut("unidad")]
        public IActionResult Put([FromBody]Unidad unidad)
        {

            if (unidad!=null)
                _unidadAppService.SaveOrUpdate(unidad);
            
            return Ok(unidad);
        }


        [HttpPut]
        public IActionResult Put([FromBody]ContenidoMateria contenidoMateria)
        {

            _contenidoMateriaAppService.SaveOrUpdate(contenidoMateria);

            if (contenidoMateria.Unidad!=null)
                _unidadAppService.SaveOrUpdate(contenidoMateria.Unidad);
        
            return Ok(contenidoMateria);
        }
        
    }
}
