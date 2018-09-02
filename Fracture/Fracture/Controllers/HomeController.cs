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
    [Produces("text/html")]
    public class HomeController : Controller
    {
        private IHostingEnvironment env;

        public HomeController(IHostingEnvironment env)
        {
            this.env = env;
        }



        [Route("index.html"), Route("")]
        public ContentResult Index()
        {
            var rootPath = this.env.ContentRootPath;

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
                    GetJavaScriptContent() + "\n" +
                    GetTemplateContent() + "\n" +
                    "</head>\n" +
                    "<body>\n" +
                    "</body>\n" +
                    "</html>"
            };
        }

        private string GetJavaScriptContent()
        {
            var sb = new StringBuilder();
            foreach (var jsinc in GetJavaScriptIncludes())
            {
                sb.Append($"<script src='{jsinc}'></script>\n");
            }
            return sb.ToString();
        }

        private string GetTemplateContent()
        {
            var rootPath = this.env.ContentRootPath;
            var templateFiles = Directory
                .GetFiles($"{rootPath}/wwwroot/js/app")
                .Where(f => f.EndsWith("-template.html"))
                .Select(LoadTemplateFile)
                .ToList();
            return String.Join("\n", templateFiles) + "\n";
        }

        private string LoadTemplateFile(string templateFile)
        {
            var name = Path.GetFileNameWithoutExtension(templateFile);
            var content = System.IO.File.ReadAllText(templateFile, Encoding.UTF8);
            return $"<script type='text/x-template' id='{name}'>{content}</script>";
        }

        private List<string> GetJavaScriptIncludes()
        {
            var jsincs = new List<string>
            {
                "js/lib/vue.js"
            };

            var rootPath = this.env.ContentRootPath;
            jsincs.AddRange(Directory
                .GetFiles($"{rootPath}/wwwroot/js/app")
                .Where(f => f.EndsWith(".js"))
                .Select(f => "js/app/" + Path.GetFileName(f)));

            return jsincs;
        }
    }
}