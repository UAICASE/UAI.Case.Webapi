using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Application;
using UAI.Case.Webapi.Config;
using UAI.Case.Domain.Common;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : UaiCaseController
    {

        ILogAppService _logAppService;
        public LogsController(ILogAppService logAppService)
        {
            _logAppService = logAppService;

        }
      
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {

            IQueryable<LogMessage> lista = _logAppService.GetAll(_ => _.Usuario).Where(p => p.IdDestino.Equals(id)).OrderByDescending(o => o.FechaCreacion);
            return Ok(lista.Take(10).ToList());
        }

      
             
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        
    }
}
