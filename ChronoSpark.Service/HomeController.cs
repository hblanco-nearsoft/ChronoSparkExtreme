using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RazorEngine;
using System.Web.Mvc;
using ChronoSpark.Data.Entities;
using ChronoSpark.Logic;
using RazorEngine.Templating;
using ChronoSpark.Data;
using System.Net.Http.Headers;
using System.IO;
using ChronoSpark.Service.Models;
using Omu.ValueInjecter;

namespace ChronoSpark.Service
{

    public class HomeController : ApiController
    {

        ResponseFormatter Formatter = new ResponseFormatter();

        [System.Web.Http.HttpGet]
        public HttpResponseMessage Index()
        {
            var model = SparkLogic.ReturnTaskList();
            string result = Razor.Resolve("Index.cshtml", model).Run(new ExecuteContext());
            var response = Request.CreateResponse(HttpStatusCode.OK);
            Formatter.FormatResponse(response,result);         
            return response;
        }
      
        public IEnumerable<TaskModel> GetTasks()
        {             
                var taskList = SparkLogic.ReturnTaskList();
                List<TaskModel> model = new List<TaskModel>();

                foreach (SparkTask task in taskList)
                {
                    var hours = task.TimeElapsed.TotalHours;
                    var roundedHours = Math.Floor(hours);
                    var hoursToPrint = roundedHours.ToString();
                    var minutes = task.TimeElapsed.Minutes.ToString();
                    var time = hoursToPrint + ":" + minutes;
                    var newTask = new TaskModel();

                    newTask.InjectFrom(task);
                    newTask.TimeInHours = time;
                    model.Add(newTask);
                    //task.TimeElapsed = TimeSpan.Parse(time);
                }
                return model;
        }

        [System.Web.Http.HttpGet]
        public TaskModel GetTaskData(SparkTask taskToFetch)
        {
            var returnedTask = SparkLogic.fetch(taskToFetch);
            var model = new TaskModel();
            model.InjectFrom(returnedTask);
            return model;        
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddTask(SparkTask task) 
        {
            var addCmd = new AddItemCmd();

            var taskToAdd = task;
            taskToAdd.StartDate = DateTime.Now;

            addCmd.ItemToWork = taskToAdd;
            addCmd.AddItem();

            return Request.CreateResponse<SparkTask>(HttpStatusCode.OK, task);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveChanges(SparkTask receivedTask)
        {
            SparkTaskBuilder builder = new SparkTaskBuilder();
            UpdateItemCmd updateCmd = new UpdateItemCmd();

            var taskToSave = builder.RebuildTask(receivedTask);
            updateCmd.ItemToWork = taskToSave;
            updateCmd.UpdateItem();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage ActivateTask(SparkTask receivedTask)
        {
            SparkTaskBuilder builder = new SparkTaskBuilder();
            UpdateItemCmd updateCmd = new UpdateItemCmd();

            var taskToActivate = builder.ReturnToActivate(receivedTask);

            TaskStateControl taskStateControl = new TaskStateControl();
            ActiveTaskProcess taskProcessor = new ActiveTaskProcess();

            if (taskToActivate == null)
            {
            
            }

            var result = taskStateControl.SetActiveTask(taskToActivate);

            if (taskToActivate != null && result == false)
            {
                taskStateControl.PauseTask();
                taskStateControl.SetActiveTask(taskToActivate);
            }

            ReminderControl.StartTime = DateTime.Now;
            taskProcessor.SetStartTime();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage PauseActiveTask()
        {
            TaskStateControl taskStateControl = new TaskStateControl();
            SparkLogic sparkLogic = new SparkLogic();
            var activeTask = sparkLogic.ReturnActiveTask();

            if (activeTask == null)
            {

            }
            taskStateControl.PauseTask();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage FinishTask(SparkTask formData)
        {
            SparkTaskBuilder builder = new SparkTaskBuilder();
            UpdateItemCmd updateCmd = new UpdateItemCmd();

            var taskToFinish = builder.ReturnToActivate(formData);

            TaskStateControl taskStateControl = new TaskStateControl();
            ActiveTaskProcess taskProcessor = new ActiveTaskProcess();

            if (taskToFinish == null)
            {
            }

            taskStateControl.FinishTask(taskToFinish);

            ReminderControl.StartTime = DateTime.Now;
            taskProcessor.SetStartTime();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        [System.Web.Http.HttpGet]
        public HttpResponseMessage FileServer(string filename)
        {
            var response = new HttpResponseMessage();
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", filename);

            response.Content = new StreamContent(File.Open(filePath, FileMode.Open));
            var parts = filename.Split('.');
            var extension = parts.Last();

            if (extension.ToLower() == "js")
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/javascript");


            return response;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage StyleServer(string filename)
        {
            var response = new HttpResponseMessage();
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Styles", filename);

            response.Content = new StreamContent(File.Open(filePath, FileMode.Open));
            var parts = filename.Split('.');
            var extension = parts.Last();

            if (extension.ToLower() == "jpg" || extension.ToLower() == "jpeg")
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            if (extension.ToLower() == "gif")
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");

            if (extension.ToLower() == "png")
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            if (extension.ToLower() == "css")
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/css");

            return response;
        }

        public static List<EventModel> listOfEvents;

        [System.Web.Http.HttpGet]
        public List<EventModel> CheckEventList()
        {
            if (listOfEvents == null)
            {
                listOfEvents = new List<EventModel>();
            }


            if (listOfEvents.Count > 0)
            {
                var savedList = new List<EventModel>(listOfEvents);
                listOfEvents.Clear();
                return savedList;
            }
            else
            {
                return listOfEvents;
            }
        }

        public static void RegisterEvent(EventModel eventToRegister)
        {
            if (listOfEvents == null) {
                listOfEvents = new List<EventModel>();
            }

            listOfEvents.Add(eventToRegister);
        }


    }
}
