using ECA.Services.Goober.Config;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECA.Services.Goober.Tasks
{
    internal class TaskManager : BackgroundService
    {
        private readonly IWorker _worker;
        private double _intervalSeconds;
        private Logger _logger;
        private IJsonConfiguration _config;

        public TaskManager(IJsonConfiguration config, IWorker worker)
        {
            _worker = worker;                             // simple task injection model
            _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            _config = config;
            _intervalSeconds = _config.TaskManagerIntervalSeconds;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            { 
                cancellationToken.Register(() => _logger.Debug($"TaskManager background task service is stopping."));

                _logger.Info("TaskManager dispatch loop started.");
                while (true)                                                                                // example of forever loop (polling)
                {
                    await Task.Delay(TimeSpan.FromSeconds( _intervalSeconds )).ContinueWith(tsk => { } );   // timer   ,  .ContinueWith() swallows the exception
                    //    _logger.Debug($"awake: TaskManager dispatch loop" );

                    //await _worker.DoTheTask();       // task manager worker routine, run asynchronously                   
                }
                _logger.Debug($"Outside the TaskManager dispatch loop.");
            }
            catch (Exception exc)
            {
                _logger.Error(exc, "TaskManager.ExecuteAsync error.");
            }
        }
    }
}
