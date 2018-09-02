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

namespace Fracture.Controllers
{
    [Produces("application/json")]
    [Route("api/FractalImage")]
    public class FractalImageController : Controller
    {

        [HttpGet]
        public FileContentResult Get([FromQuery] FractalImageInput input)
        {
            Console.WriteLine(input.ToString());
            var fgen = new MandelbrotGenerator();
            var image = fgen.Generate(input);

            var result = new FileContentResult(image, "image/png");
            return result;
        }
    }
}