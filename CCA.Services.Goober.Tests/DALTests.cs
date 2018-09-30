using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using CCA.Services.Goober.Models;
using System.Net.Http;
using DataModels = CCA.Services.Goober.DAL.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CCA.Services.Goober.Exceptions;
using CCA.Services.Goober;
using CCA.Services.Goober.Config;
using CCA.Services.Goober.Service;
using CCA.Services.Goober.DAL;

namespace CCA.Services.Document.Goober.Tests
{
    [TestClass]
    public class DALTests     // data access layer (DAL) integration test(s)
    {
        IRepository _repo;

        [TestInitialize]
        public void Initialize()
        {
            IJsonConfiguration config = new JsonConfiguration();
            _repo = new Repository(config.ConnectionString);
        }

        //[IgnoreAttribute]
        [TestMethod]
        public void RepoGetAll()
        {
            List<DataModels.PeanutButter> peanutButters = _repo.ReadAllPeanutButters();
        }

    }
}
