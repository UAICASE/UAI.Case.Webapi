using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UAI.Case.Webapi.Config;
using Microsoft.AspNetCore.Http;
using UAI.Case.Domain.Common;
using Microsoft.Net.Http.Headers;
using UAI.Case.Application;
using System.IO;



using Microsoft.Extensions.PlatformAbstractions;
//using Microsoft.DotNet.InternalAbstractions;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace UAI.Case.Webapi.Controllers
{
    [Route("api/[controller]")]
    public class FileController : UaiCaseController
    {
        IArchivoAppService _archivoAppService;
        IHostingEnvironment _environment;
        public FileController(IArchivoAppService archivoAppService, IHostingEnvironment environment)
        {
            _archivoAppService = archivoAppService;
            _environment = environment;
        }


        [HttpGet("{id}")]
        public IActionResult GetFile(Guid id)
        {

            try
            {
                string fileName = Path.Combine(_environment.WebRootPath, "Files", id.ToString());
                //byte[] content = System.IO.File.ReadAllBytes(filename);
                //return Ok(content);
                byte[] buffer = null;
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                }

                //return File(buffer, MediaTypeNames.Application.Octet);
                return File(buffer, "Octet");


            }
            catch (Exception)
            {

                return InternalServerError("");
            }
          
            

       
            
           
        }



        [HttpPut]
        public async Task<IActionResult> PutFile()
        {
            try
            {
                Archivo archivo = new Archivo();
                if (HttpContext.Request.Form.Files.Count == 1)
                {
                    var file = HttpContext.Request.Form.Files[0];

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    archivo.Nombre = fileName;
                    _archivoAppService.SaveOrUpdate(archivo);

                    //var targetDirectory = Path.Combine(_environment.ContentRootPath, "Files",archivo.Id.ToString());

                    using (var fileStream = new FileStream(Path.Combine(_environment.WebRootPath, "Files", archivo.Id.ToString()), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }


                    //await file.CopyToAsync(targetDirectory);
                    return Ok(archivo);

                }
                else
                    return NotFoundResult(archivo); 
            }
            catch (Exception e)
            {
                
                return InternalServerError(e.Message);
            }
         
          


    
        }
    }
     
    
}
