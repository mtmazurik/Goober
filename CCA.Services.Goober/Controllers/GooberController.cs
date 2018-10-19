using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using CCA.Services.Goober.Models;
using CCA.Services.Goober.JsonHelpers;
using CCA.Services.Goober.Service;
using CCA.Services.Goober.Exceptions;
using CCA.Services.Goober.Logging.Models;
using Microsoft.Extensions.Logging;

namespace CCA.Services.Goober.Controllers
{
    [Route("/")]
    public class GooberController : Controller
    {
        private CustomLoggerDBContext _context;
        private readonly ILogger<GooberController> _logger;
        public GooberController( ILogger<GooberController> logger)   // also can inject,  ... CustomLoggerDBContext context )   to log database operations automatically
        {
            //_context = context;
            _logger = logger;
        }
       
        [HttpGet("ping")]   // ping
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Response))]
        public IActionResult GetPing()
        {
            _logger.LogInformation("GET ping");
            return ResultFormatter.ResponseOK((new JProperty("Ping", "Success")));
        }
        [HttpGet("version")]   // service version (from compiled assembly version)
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Response))]
        public IActionResult GetVersion()
        {
            _logger.LogInformation("GET version");
            var assemblyVersion = typeof(Startup).Assembly.GetName().Version.ToString();
            return ResultFormatter.ResponseOK((new JProperty("Version", assemblyVersion)));
        }
        [HttpGet("recipe/peanutbutter")]
        [Authorize]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Response))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, typeof(Response))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, typeof(Response))]
        public IActionResult GetRecipePeanutButter([FromServices]IRecipe recipe)
        {
            try
            {
                IResponse response = null;

                recipe.GetPeanutButterRecipe();
                return (ResultFormatter.Format(200, response));         // ok
            }
            catch (NYIException exc)
            {
                return (ResultFormatter.Format(501, exc));          // not yet implemented
            }
            catch (Exception exc)                                       // catch-all exception try/catch block
            {
                _logger.LogError(exc, "GET recipe/peanutbutter unexpected error.");
                return ResultFormatter.Format(500, exc);
            }
        }
    }
}
