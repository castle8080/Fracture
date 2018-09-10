using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

namespace Fracture.Controllers
{
    /// <summary>
    /// Send the user to the home page.
    /// </summary>
    [Produces("text/html")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return new RedirectResult("/index.html");
        }
    }
}