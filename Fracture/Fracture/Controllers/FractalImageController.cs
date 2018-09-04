using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Fracture.Fractal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fracture.Controllers
{
    [Produces("application/json")]
    [Route("api/image")]
    public class FractalImageController : Controller
    {

        [HttpGet]
        public async Task Get([FromQuery] FractalImageSettings input)
        {
            Console.WriteLine(input.ToString());
            var fgen = new MandelbrotGenerator();
            var image = fgen.Generate(input);

            var imageProperties = JsonConvert.SerializeObject(input);

            var response = this.HttpContext.Response;
            response.Headers.Add("X-GeneratedImageProperties", imageProperties);
            response.ContentType = "image/png";
            await response.Body.WriteAsync(image, 0, image.Length);
        }
    }
}