using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Fracture.Controllers
{
    [Produces("text/html")]
    public class HomeController : Controller
    {
        [Route("index.html"), Route("")]
        public ContentResult Index()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.Accepted,
                Content =
                    "<!DOCTYPE html>\n" +
                    "<html>\n" +
                    "<head>\n" +
                    "<meta charset = 'UTF-8'>\n" +
                    "<title>Fracture</title>\n" +
                    "</head>\n" +
                    "<body>\n" +
                    "</body>\n" +
                    "</html>"
            };
        }
    }
}