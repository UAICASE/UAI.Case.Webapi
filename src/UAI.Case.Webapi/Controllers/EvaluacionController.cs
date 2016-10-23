using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Domain.CASE;
using UAI.Case.Application;
using UAI.Case.Domain.Enums;
using UAI.Case.Webapi.hubs;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using UAI.Case.Dto;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class EvaluacionController : UaiCaseController
    {
        IEvaluacionAppService _evaluacionAppService;
        IRespuestaEvaluacionAppService _respuestaEvaluacionAppService;
        IHubContext _hubContext;

        public EvaluacionController(IEvaluacionAppService evaluacionAppService, IConnectionManager connectionManager, IRespuestaEvaluacionAppService respuestaEvaluacionAppService)
        {
            _evaluacionAppService = evaluacionAppService;
            _respuestaEvaluacionAppService = respuestaEvaluacionAppService;
            _hubContext = connectionManager.GetHubContext<MessageHub>();
        }


        // GET api/values/5
        [HttpPut]
        public IActionResult Put([FromBody] Evaluacion evaluacion)
        {
            _evaluacionAppService.SaveOrUpdate(evaluacion);
            return Ok(evaluacion);
        }

        [HttpGet("{id}")]
        public IActionResult GetEvaluation(Guid id)
        {
            var evaluaciones  =AutoMapper.Mapper.Map<IList<EvaluacionDTO>>( _evaluacionAppService.GetAll(_ => _.Usuario).Where(o => o.ModeloId == id).ToList());


            foreach (var item in evaluaciones)
            {
                item.Respuestas = _respuestaEvaluacionAppService.GetAll(_ => _.Usuario).Where(o => o.EvaluacionId==item.Id).ToList();
            }

            //var evaluaciones = _evaluacionAppService.GetEvaluacionesConRespuestas(id);
            return Ok(evaluaciones);
        }

        [HttpGet("diagrama/{id}")]
        public IActionResult GetAllEnCursoFromDiagram(Guid id)
        {
            var evaluaciones = _evaluacionAppService.GetAll(_ => _.Usuario).Where(o => o.DiagramaId == id && o.Estado==EstadoEvaluacion.EnCurso).ToList();
            return Ok(evaluaciones);
        }


    


        [HttpPost("finalizar/{id}")]
        public IActionResult EndEvaluation(Guid id)
        {
            Evaluacion evaluacion = _evaluacionAppService.Get(id);

            if (evaluacion != null && evaluacion.Estado == EstadoEvaluacion.EnCurso)
            {

                evaluacion.Estado = EstadoEvaluacion.Finalizada;
                _evaluacionAppService.SaveOrUpdate(evaluacion);

                //avisar a los clientes del diagrama
                
                _hubContext.Clients.Group(evaluacion.DiagramaId.ToString()).endEvaluationMessage(AutoMapper.Mapper.Map<Evaluacion>(evaluacion));

                
               
            }


            return Ok(evaluacion);

        }


        [HttpPost("activar/{id}")]
        public IActionResult ActivateEvaluation(Guid id)
        {
            Evaluacion evaluacion = _evaluacionAppService.Get(id);

            if (evaluacion!=null && evaluacion.Estado==EstadoEvaluacion.Pendiente)
            {
                
                evaluacion.Estado = EstadoEvaluacion.EnCurso;
                _evaluacionAppService.SaveOrUpdate(evaluacion);

                //avisar a los clientes del diagrama
                _hubContext.Clients.Group(evaluacion.DiagramaId.ToString()).newEvaluationMessage(AutoMapper.Mapper.Map<Evaluacion>(evaluacion));
            }


            return Ok(evaluacion);
            
        }

        [HttpPut("respuesta/")]
        public IActionResult PutAnswertToComment([FromBody] RespuestaEvaluacion respuesta)
        {

            //newEvaluationResponseMessage
            _respuestaEvaluacionAppService.SaveOrUpdate(respuesta);

            
            _hubContext.Clients.Group(respuesta.ModeloId.ToString()).newEvaluationResponseMessage(respuesta);
            
            return Ok(respuesta);
        }

    }
}
