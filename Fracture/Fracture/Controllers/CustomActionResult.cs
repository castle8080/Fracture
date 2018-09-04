using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fracture.Controllers
{
    public class CustomActionResult : IActionResult
    {
        private Func<ActionContext, Task> callback;

        public CustomActionResult(Func<ActionContext, Task> callback)
        {
            this.callback = callback;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return callback(context);
        }
    }
}
