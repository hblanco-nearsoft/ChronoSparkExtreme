﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Logic;
using System.Threading;

namespace ChronoSpark.Service
{
    public partial class ChronoSparkService : ServiceBase
    {
        public ChronoSparkService()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }

        protected override void OnStart(string[] args)
        {

            //ThreadPool.QueueUserWorkItem(delegate { SparkLogic.Initialize(); });
            ReminderControl defaultController = new ReminderControl();
            ThreadPool.QueueUserWorkItem(delegate { defaultController.ActivateReminders(); });

            eventLog1.WriteEntry("The Service has started");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("The Service has ended");
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}