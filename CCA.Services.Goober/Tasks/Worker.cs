using DataModels = CCA.Services.Goober.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CCA.Services.Goober.Config;
using CCA.Services.Goober.DAL;
using CCA.Services.Goober.Models;
using System.Threading;
//using CCA.Services.Goober.DAL.Models;
using Microsoft.Extensions.Logging;

namespace CCA.Services.Goober.Tasks
{
    public class Worker : IWorker
    {
        private ILogger _logger;
        private IJsonConfiguration _config;
        private IRepository _repo;

        public Worker(IJsonConfiguration config, ILogger logger)                 //ctor
        {
            _logger = logger;
            _config = config;
            _repo = new Repository(config.ConnectionString);
        }

        public async Task DoTheTask()
        {
            try
            {
                await Task.Run(() => MeaningfulWork());
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, "DoTheTask() task error, while attempting MeaningfulWork() async method call.");
            }
        }

        private void MeaningfulWork()
        {
            List<PeanutButter> peanutButters;

            try
            {
               //peanutButters = _repo.ReadAllPeanutButters();   // code that executes, as a periodic scheduled worker task
            }
            catch(Exception exc)
            {
                _logger.LogError(exc, "Error reading peanut butter table.");
                throw exc;
            }
        }
    }
}
