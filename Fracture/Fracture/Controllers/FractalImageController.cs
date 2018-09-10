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
        const string GeneratedImagePropertiesKey = "X-GeneratedImageProperties";
        const string MimeTypePNG = "images/png";

        /// <summary>
        /// Generates a fractal image.
        /// </summary>
        [HttpGet]
        public async Task Get([FromQuery] FractalImageSettings input)
        {
            Console.WriteLine(input.ToString());
            var response = this.HttpContext.Response;

            // Generate the image
            var fgen = new MandelbrotGenerator();
            var image = fgen.Generate(input);

            // Get the image properties and add them to the response header.
            var imageProperties = JsonConvert.SerializeObject(input);
            response.Headers.Add(GeneratedImagePropertiesKey, imageProperties);

            response.ContentType = MimeTypePNG;
            await response.Body.WriteAsync(image, 0, image.Length);
        }
    }
}