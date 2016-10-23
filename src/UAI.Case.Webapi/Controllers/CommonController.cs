using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Domain.Enums;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class CommonController : UaiCaseController
    {
        // GET: api/values
        [HttpGet("turnos")]
        public IActionResult GetTurnos()
        {
            return Ok(Enum.GetValues(typeof(Turno)));
        }
        [HttpGet("tipo-nota")]
        public IActionResult GetTipoNota()
        {
            return Ok(Enum.GetValues(typeof(TipoNota)));
        }

        [HttpGet("diagramas")]
        public IActionResult GetDiagramas()
        {
            return Ok(Enum.GetValues(typeof(TipoDiagrama)));
        }

        [HttpGet("dias")]
        public IActionResult GetDias()
        {
            return Ok(Enum.GetValues(typeof(Dia))   );
        }

        [HttpGet("sedes")]
        public IActionResult GetSedes()
        {
            return Ok(Enum.GetValues(typeof(Sede)));
        }

        [HttpGet("comisiones")]
        public IActionResult GetTipoComision()
        {
            return Ok(Enum.GetValues(typeof(TipoComision)));
        }

        [HttpGet("tipo-contenido")]
        public IActionResult GetTipoContenido()
        {
            return Ok(Enum.GetValues(typeof(TipoContenido)));
        }

        [HttpGet("tipo-clase")]
        public IActionResult GetTipoClase()
        {
            return Ok(Enum.GetValues(typeof(TipoClase)));
        }

        [HttpGet("tipo-visibilidad-curso")]
        public IActionResult GetTipoVisibilidadCurso()
        {
            return Ok(Enum.GetValues(typeof(TipoVisibilidadCurso)));
        }
    }
}
