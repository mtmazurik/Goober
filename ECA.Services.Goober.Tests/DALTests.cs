using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using ECA.Services.Goober.Models;
using System.Net.Http;
using DataModels = ECA.Services.Goober.DAL.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ECA.Services.Goober.Exceptions;
using ECA.Services.Goober;
using ECA.Services.Goober.Config;
using ECA.Services.Goober.Service;
using ECA.Services.Goober.DAL;

namespace ECA.Services.Document.Goober.Tests
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
