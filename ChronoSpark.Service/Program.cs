using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Logic;
using System.Threading;

namespace ChronoSpark.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ReminderControl defaultController = new ReminderControl();
            ThreadPool.QueueUserWorkItem(delegate 
            {
                SparkLogic.Initialize();
                defaultController.ActivateReminders(); 
            });

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ChronoSparkService() 
            };
            ServiceBase.Run(ServicesToRun);            
        }
    }
}
