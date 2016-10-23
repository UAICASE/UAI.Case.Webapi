using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Domain.Common;
using UAI.Case.Application;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using UAI.Case.Webapi.hubs;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : UaiCaseController
    {

        IChatAppService _chatAppService;
        IUsuarioAppService _usuarioAppService;
        IHubContext _hubContext;


        public ChatController(IChatAppService chatAppService, IUsuarioAppService usuarioAppService, IConnectionManager connectionManager)
        {
            _chatAppService = chatAppService;
            _usuarioAppService = usuarioAppService;
            _hubContext = connectionManager.GetHubContext<MessageHub>();
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {

            //los ultimos 100 mensajes de chat
            IList<ChatMessage> chats = _chatAppService.GetAll(_=>_.Usuario).Where(p => p.IdDestino.Equals(id)).ToList();
                 


            return Ok(chats);
        }



        [HttpPost("read/{id}")] //id mensaje
        public IActionResult ReadOne(Guid id)
        {
            ChatMessage chat = _chatAppService.Get(id);
            if (chat != null)
            {


                if (!chat.IsReaded(UsuarioId))
                {
                    chat.ReadedBy += "-" + UsuarioId.ToString();
                    _chatAppService.SaveOrUpdate(chat);
                }
            }

            //_hubContext.Clients.Group(message.IdDestino.ToString()).newChatMessage(message);
            return Ok(id);
        }

        [HttpPost("read-all/{idCanal}")] 
        public IActionResult ReadAll(Guid idCanal)
        {

            IList<ChatMessage> chats = _chatAppService.GetAll().Where(i => i.IdDestino.Equals(idCanal)).ToList();

            foreach (var chat in chats)
            {
                if (!chat.IsReaded(UsuarioId))
                {
                    chat.ReadedBy += "-" + UsuarioId.ToString();
                    _chatAppService.SaveOrUpdate(chat);
                }
                
            }

            return Ok(idCanal);
            //_hubContext.Clients.Group(message.IdDestino.ToString()).newChatMessage(message);

        }

        [HttpPut]
        public void Put([FromBody]ChatMessage message)
        {
            message.ReadedBy += "-" + UsuarioId.ToString();
            _chatAppService.SaveOrUpdate(message);
            _hubContext.Clients.Group(message.IdDestino.ToString()).newChatMessage(message);
            
        }

        
    }
}
