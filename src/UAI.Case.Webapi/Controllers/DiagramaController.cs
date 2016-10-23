using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;

using UAI.Case.Application;
using UAI.Case.Domain.Proyectos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Text;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class DiagramaController : UaiCaseController
    {
        IModeloAppService _modeloAppService;
        IElementoAppService _elementoAppService;
        public DiagramaController(IElementoAppService elementoAppService,IModeloAppService modeloAppService)
        {
            _elementoAppService = elementoAppService;
            _modeloAppService = modeloAppService;

        }

        
       

        // GET api/values/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public  JsonResult Get(string id)
        {
            //Elemento elemento = _elementoAppService.Get(id, _ => _.Elementos);
            //if (elemento.IdModelo != Guid.Empty)
            //{
            //    byte[] json= _modeloAppService.Get(elemento.IdModelo).Model;
            //   JObject j =  JObject.Parse(Encoding.UTF8.GetString(json));
            //    return Json(j);
            //}

            ////string json =  await MongoDBManager.GetDiagram(id);

            return Json(String.Empty);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }



        [HttpPut]
        public void Put(Elemento elemento)
        {
            //Elemento elemento = _elementoAppService.Get(id, _ => _.Elementos, _ => _.Usuario);

            //if (elemento != null && !elemento.IsFolder)
            //{

             
            //    _elementoAppService.SaveOrUpdate(elemento);
             
            //}



        }

        // PUT api/values/5
        [HttpPut("{id}")]
     public  void Put (Guid id,[FromBody]JObject value)
        {


            //Elemento m = _elementoAppService.Get(id);

            //if (m != null )
            //{
            //    Modelo modelo;
            //    if (m.IdModelo == Guid.Empty)
            //        modelo = new Modelo();
            //    else
            //        modelo = _modeloAppService.Get(m.IdModelo);    
                
            //        modelo.Model =  System.Text.Encoding.UTF8.GetBytes(value.ToString());

            //    _modeloAppService.SaveOrUpdate(modelo);
            //    m.IdModelo = modelo.Id;
            //    _elementoAppService.SaveOrUpdate(m);
             
            //}
         


        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
