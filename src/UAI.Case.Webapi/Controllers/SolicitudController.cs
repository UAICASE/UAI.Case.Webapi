using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using UAI.Case.Webapi.Config;
using UAI.Case.Application;
using UAI.Case.Domain.Academico;
using UAI.Case.Domain.Roles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using UAI.Case.Dto;
using AutoMapper;
using UAI.Case.Domain.Common;
using UAI.Case.Domain.Enums;
using UAI.Case.Webapi.hubs;
using Microsoft.AspNetCore.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class SolicitudController : UaiCaseController
    {


        //refactorizar pra otras solicitudes
        IJoinCursoRequestAppService _joinCursoAppService;



        IHubContext _hubContext;
        ILogAppService _logAppService;
        ICursoAppService _cursoAppService;
        IAlumnoAppService _alumnoAppService;
        IDocenteAlumnoCursoAppService _alumnoCursoAppService;

        public SolicitudController(IDocenteAlumnoCursoAppService alumnoCursoAppService, ICursoAppService cursoAppService, ILogAppService logAppService, IAlumnoAppService alumnoAppService, IConnectionManager connectionManager, IDocenteAlumnoCursoAppService docenteAlumnoCursoAppService, IJoinCursoRequestAppService joinCursoAppService)
        {

            _cursoAppService = cursoAppService;
            _joinCursoAppService = joinCursoAppService;
            _hubContext = connectionManager.GetHubContext<MessageHub>();
            _logAppService = logAppService;
            _alumnoAppService = alumnoAppService;
            _alumnoCursoAppService = alumnoCursoAppService;

        }

        [HttpGet("curso/{id}")]
        public IActionResult GetCurso(Guid id)
        {

            return Ok(_joinCursoAppService.GetAll(_ => _.Usuario).Where(x => x.CursoId.Equals(id)));
        }

      
    }
}

