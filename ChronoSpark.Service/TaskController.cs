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
using System.Net.Http.Formatting;


namespace ChronoSpark.Service
{
    public class TaskController : ApiController
    {
        ResponseFormatter Formatter = new ResponseFormatter();

        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddTask(FormDataCollection formData)
        {
            SparkTaskBuilder builder = new SparkTaskBuilder();
            AddItemCmd addCmd = new AddItemCmd();


            var taskToSave = builder.BuildTask(formData);
            addCmd.ItemToWork = taskToSave;
            addCmd.AddItem();
            
            //var tasks = SparkLogic.ReturnTaskList();
            //String result = Razor.Resolve("GetAllTasks.cshtml", tasks).Run(new ExecuteContext());
            //var res = Request.CreateResponse(HttpStatusCode.OK);
            //Formatter.FormatResponse(res, result);
            
            var resultToReturn = GetAllTasks();
            return resultToReturn;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAllTasks()
        {
            var tasks = SparkLogic.ReturnTaskList();
            String result = Razor.Resolve("GetAllTasks.cshtml", tasks).Run(new ExecuteContext());

            var res = Request.CreateResponse(HttpStatusCode.OK);
            Formatter.FormatResponse(res, result);
            return res;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetActiveTask() 
        {
            SparkLogic logic = new SparkLogic();
            var activeTask = logic.ReturnActiveTask();
            String result = Razor.Resolve("GetActiveTask.cshtml", activeTask).Run(new ExecuteContext());

            var res = Request.CreateResponse(HttpStatusCode.OK);
            Formatter.FormatResponse(res, result);
            return res;
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage GetByID(FormDataCollection formData) 
        {
            var id = formData.ElementAt(0).Value;
            SparkTask taskToFetch = new SparkTask { Id = id };
            var retrievedTask = SparkLogic.fetch(taskToFetch) as SparkTask;

            String result = Razor.Resolve("EditTask.cshtml", retrievedTask).Run(new ExecuteContext());
            var res = Request.CreateResponse(HttpStatusCode.OK);
            Formatter.FormatResponse(res, result);

            return res;
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage SaveChanges(FormDataCollection formData)
        {
            SparkTaskBuilder builder = new SparkTaskBuilder();
            UpdateItemCmd updateCmd = new UpdateItemCmd();

            var taskToSave = builder.RebuildTask(formData);
            updateCmd.ItemToWork = taskToSave;
            updateCmd.UpdateItem();
 
            return GetAllTasks();
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage ActivateTask(FormDataCollection formData) 
        {
            SparkTaskBuilder builder = new SparkTaskBuilder();
            UpdateItemCmd updateCmd = new UpdateItemCmd();

            var taskToActivate = builder.ReturnToActivate(formData);

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

            return GetAllTasks();
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage PauseActiveTask(FormDataCollection formData)
        {
           TaskStateControl taskStateControl = new TaskStateControl();
            SparkLogic sparkLogic = new SparkLogic();
            var activeTask = sparkLogic.ReturnActiveTask();

            if (activeTask == null) 
            {
                
            }
            taskStateControl.PauseTask();
   
            return GetAllTasks();
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage FinishTask(FormDataCollection formData)
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

            return GetAllTasks();
        }
    }
}
