﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using NLog;
using ECA.Services.Goober.Models;
using ECA.Services.Goober.JsonHelpers;
using ECA.Services.Goober.Service;
using ECA.Services.Goober.Exceptions;

namespace ECA.Services.Goober.Controllers
{
    [Route("/")]
    public class GooberController : Controller
    {
        private Logger _logger;
        public GooberController()
        {
            _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
        }

        [HttpGet("recipe/peanutbutter")]
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
            catch ( NYIException exc )
            {
                    return (ResultFormatter.Format(501, exc));          // not yet implemented
            }
            catch( Exception exc)                                       // catch-all exception try/catch block
            {
                _logger.Error(exc, "GET recipe/peanutbutter unexpected error.");
                return ResultFormatter.Format(500, exc);
            }
        }
        
        [HttpGet("ping")]   // ping
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Response))]
        public IActionResult GetPing()
        {
            _logger.Info("GET ping");
            return ResultFormatter.ResponseOK((new JProperty("Ping", "Success")));
        }
        [HttpGet("version")]   // service version (from compiled assembly version)
        [AllowAnonymous]
        [SwaggerResponse((int)HttpStatusCode.OK, typeof(Response))]
        public IActionResult GetVersion()
        {
            _logger.Info("GET version");
            var assemblyVersion = typeof(Startup).Assembly.GetName().Version.ToString();
            return ResultFormatter.ResponseOK((new JProperty("Version", assemblyVersion)));
        }

    }
}
