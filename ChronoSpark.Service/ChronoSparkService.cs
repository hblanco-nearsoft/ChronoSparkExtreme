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
using RazorEngine;
using RazorEngine.Templating;
using System.IO;
using System.Reflection;
using RazorEngine.Configuration;
using ChronoSpark.Service.Static.File.Handlers;
using System.Web.Routing;

namespace ChronoSpark.Service
{
    public partial class ChronoSparkService : ServiceBase
    {
        private HttpSelfHostServer _server;
        private readonly HttpSelfHostConfiguration _config;
        public const string ServiceAddress = "http://localhost:8080"; //TODO: Move this to config file
        public ListenerControl listeners = new ListenerControl();
        ReminderControl reminderControl = new ReminderControl();

        public ChronoSparkService()
        {
            InitializeComponent();
            listeners.Suscribe(reminderControl);

            _config = new NtlmSelfHostConfiguration(ServiceAddress);

            _config.Routes.MapHttpRoute("StaticJS", "Scripts/{controller}/{action}/{filename}", new { controller = "Home", action = "FileServer" });

            _config.Routes.MapHttpRoute("EventLIst", "Events/{controller}/{action}", new { controller = "Home", action = "CheckEventList" });
            

            _config.Routes.MapHttpRoute("DefaultApi",
                "chronospark/{controller}/{action}",
                new {controller = "home", action = "something", id = RouteParameter.Optional });

            _config.Routes.MapHttpRoute(
            "Default", "{controller}/{action}/",
            new { controller = "Home", action = "Index" }); 
            
            string viewPathTemplate = "ChronoSpark.Service.Views.{0}";
            TemplateServiceConfiguration templateConfig = new TemplateServiceConfiguration();
            templateConfig.Resolver = new DelegateTemplateResolver(name =>
            {
                string resourcePath = string.Format(viewPathTemplate, name);
                var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            });

            Razor.SetTemplateService(new TemplateService(templateConfig));
            
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
            SparkLogic.Initialize();
            ReminderControl defaultController = new ReminderControl();
            ThreadPool.QueueUserWorkItem(delegate { defaultController.ActivateReminders(); });
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