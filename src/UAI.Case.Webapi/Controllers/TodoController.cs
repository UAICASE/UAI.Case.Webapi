using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using Microsoft.AspNetCore.Hosting;
using UAI.Case.Domain;
using UAI.Case.Domain.Common;
using Microsoft.AspNetCore.Http;
using UAI.Case.Domain.Academico;
using UAI.Case.Webapi.hubs;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using System.Collections;
using UAI.Case.Dto;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : UaiCaseController
    {
        
        IHubContext _hubContext;
        ITodoAppService _todoAppService;
              
        ILogAppService _logAppService;
        
        public TodoController(ITodoAppService todoAppService,IConnectionManager connectionManager,ILogAppService logAppService)
        {
            _logAppService= logAppService; 
            _todoAppService = todoAppService;
        
            _hubContext = connectionManager.GetHubContext<MessageHub>();
        }
        // GET: api/values
       

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
         //trae todos los Todo de un channel 
            return Ok(AutoMapper.Mapper.Map<IList<Todo>>(_todoAppService.GetAll().Where(p => p.ChannelId.Equals(id)).ToList()));

        }

        static protected object _locker = new object();

        [HttpGet("todo")]
        public IActionResult GetPendientesGrupo()
        {

          
            long pend;
            lock (_locker)
            {
                pend = _todoAppService.GetPendientes(UsuarioId);
            }

          

            return Ok(AutoMapper.Mapper.Map<IList<Todo>>(pend));

        }


        [HttpPut]
        public IActionResult Put([FromBody]Todo todo)
        {


            bool hasId = !todo.Id.Equals(Guid.Empty);
            bool cambioEstado = todo.Done != todo.EstadoAnterior;

            
                todo.EstadoAnterior = todo.Done;
          
            _todoAppService.SaveOrUpdate(todo);

            LogMessage log = new LogMessage();
            //log.IdDestino = todo.Grupo.Id;
             log.IdDestino = todo.ChannelId;
            log.Tag = Domain.Enums.TipoTagLog.TagGrupoTodo;

            _logAppService.SaveOrUpdate(log);
            

            //creo log
            String[] connections = MessageHub.GetConnectionMapping().GetConnections(UsuarioId.ToString()).ToArray();
            String id = String.Empty;
            if (connections.Count() == 1)
                id = connections[0];


            if (hasId) //
            {
             //   _hubContext.Clients.Group(todo.Grupo.Id.ToString(), id).newGrupoTodo(todo);

                if (cambioEstado)
                    if (todo.Done)
                        log.Mensaje = "TODO Finalizado";
                    else
                        log.Mensaje = "TODO Reactivado" ;
                else
                    log.Mensaje = "TODO modificado"  ;
              
            }
            else
            {
               // _hubContext.Clients.Group(todo.Grupo.Id.ToString(), id).newGrupoTodo(todo);
                log.Mensaje = "Nuevo TODO";
            }

            log.Mensaje2 = todo.Title;
            //_hubContext.Clients.Group(todo.Grupo.Id.ToString()).newGrupoTodo(todo);
            _hubContext.Clients.Group(todo.ChannelId.ToString()).newTodo(todo);
            _logAppService.SaveOrUpdate(log);
            //_hubContext.Clients.Group(todo.Grupo.Id.ToString()).newGrupoLog(log);
            _hubContext.Clients.Group(todo.ChannelId.ToString()).newLog(log);

            return Ok(AutoMapper.Mapper.Map<Todo>(todo));
        }



    
    }
}
