using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using UAI.Case.Domain.Common;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using UAI.Case.Webapi.hubs;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class MailController : UaiCaseController
    {
      
        IHubContext _hubContext;
        IMailAppService _mailAppService;


      
        public MailController(IMailAppService mailAppService,  IConnectionManager connectionManager)
        {
            _mailAppService = mailAppService;
            _hubContext = connectionManager.GetHubContext<MessageHub>();
            
        }
    
        // GET: api/values
        [HttpGet("{page}/{count}")]
        public IActionResult GetCount(int page, int count)
        {
            

    
            var resp = Mapper.Map<List<Mail>, List<Mail>>(_mailAppService.GetAll().Where(p => p.MailTo.Equals(UsuarioId)).OrderByDescending(p=>p.FechaCreacion).Skip((page - 1) * count).Take(count).ToList());
            return Ok(resp);
        }

        [HttpGet("inbox")]
        public IActionResult Get()
        {
            
            var resp = Mapper.Map<List<Mail>, List<Mail>>(_mailAppService.GetAll().Where(p => p.MailTo.Equals(UsuarioId)).OrderByDescending(p => p.FechaCreacion).ToList());
            return Ok(resp);
        }


        [HttpGet("{id}")]
        public IActionResult GetOneMail(Guid id)
        {
            
            var resp = _mailAppService.GetAll().Where(p => p.MailTo.Equals(UsuarioId) && p.Id.Equals(id) );
            return Ok(Mapper.Map<List<Mail>, List<Mail>>(resp.ToList()));
        }

        // GET api/values/5
        [HttpGet("new")]
        public IActionResult GetNewMail()
        {
            
            var resp = _mailAppService.GetAll().Where(p => p.MailTo.Equals(UsuarioId) && p.ReadDate==null);
            return Ok(Mapper.Map<List<Mail>, List<Mail>>(resp.ToList()));
        }

        // POST api/values
        //read mail
        [HttpPost]
        public IActionResult Post([FromBody]Mail mail)
        {
            mail.ReadDate = DateTime.Now;
            _mailAppService.SaveOrUpdate(mail);
          
               
             
                String[] connections = MessageHub.GetConnectionMapping().GetConnections(mail.Usuario.Id.ToString()).ToArray();
            foreach (string item in connections)
            {
                
                _hubContext.Clients.Client(item).readMailMessage(mail);
            }

      connections = MessageHub.GetConnectionMapping().GetConnections(mail.MailToUsuario.Id.ToString()).ToArray();
            foreach (string item in connections)
            {

                _hubContext.Clients.Client(item).readMailMessage(mail);
            }




            return Ok(mail);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Mail mail)
        {
            _mailAppService.SaveOrUpdate(mail);


            String[] connections = MessageHub.GetConnectionMapping().GetConnections(mail.MailTo.ToString()).ToArray();
            foreach (string item in connections)
            {
                _hubContext.Clients.Client(item).newMailMessage(mail);
            }
            

            return Ok(mail);
        }


        

            [HttpGet("outbox")]
        public IActionResult GetOutbox()
        {
            
            var resp = Mapper.Map<List<Mail>, List<Mail>>(_mailAppService.GetAll().Where(p => p.Usuario.Id.Equals(UsuarioId)).OrderByDescending(p => p.FechaCreacion).ToList());
            return Ok(resp);
        }



        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
