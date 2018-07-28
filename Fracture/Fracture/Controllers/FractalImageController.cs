using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fracture.Controllers
{
    [Produces("application/json")]
    [Route("api/FractalImage")]
    public class FractalImageController : Controller
    {

        [HttpGet]
        public string Get()
        {

            return "foo";
        }

    }
}