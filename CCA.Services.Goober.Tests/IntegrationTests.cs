using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCA.Services.Goober.Exceptions;
using CCA.Services.Goober;
using CCA.Services.Goober.Config;
using Newtonsoft.Json.Linq;
using System;
using CCA.Services.Goober.Models;
using System.Net.Http;
using CCA.Services.Goober.DAL;

namespace CCA.Services.Document.Goober.Tests
{
    [TestClass]
    public class IntegrationTests
    {  
        IJsonConfiguration _config;
        IRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            _config = new JsonConfiguration();
            _repository = new Repository(_config.ConnectionString);
        }

        // here we might use RESTSharp for API calls (tests) to be automated (integrated)

        // If using these they run during the build, and must be maintained (or the build will remain broken on failed integration tests,
        // that the developer creates
    }
}
