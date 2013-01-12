using System;
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
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Net.Http;

namespace ChronoSpark.Service
{
    public partial class ChronoSparkService : ServiceBase
    {
        private HttpSelfHostServer _server;
        private readonly HttpSelfHostConfiguration _config;
        public const string ServiceAddress = "http://localhost:8080";

        public ChronoSparkService()
        {
            InitializeComponent();

            _config = new HttpSelfHostConfiguration(ServiceAddress);

            _config.Routes.MapHttpRoute("DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            _config.Routes.MapHttpRoute(
            "Default", "{controller}/{id}",
            new { id = RouteParameter.Optional , action = "SayHello" }); 

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
            ReminderControl defaultController = new ReminderControl();
            ServiceListener serviceListener = new ServiceListener();
            ThreadPool.QueueUserWorkItem(delegate { defaultController.ActivateReminders(); });
            ThreadPool.QueueUserWorkItem(delegate { serviceListener.ActivateListener(); });

            _server = new HttpSelfHostServer(_config);
            _server.OpenAsync();        

            eventLog1.WriteEntry("The Service has started");
        }

        protected override void OnStop()
        {
            _server.CloseAsync().Wait();
            _server.Dispose();
            eventLog1.WriteEntry("The Service has stopped");
        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }
    }
}