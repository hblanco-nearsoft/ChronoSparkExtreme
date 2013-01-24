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

        public SparkTask TaskByID(String id)
        {
            IRavenEntity entityToFetch = new SparkTask { Id = "SparkTasks/" + id };

            var task = SparkLogic.fetch(entityToFetch) as SparkTask;

            return task;
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddTask(FormDataCollection formData)
        {
            SparkTaskBuilder builder = new SparkTaskBuilder();
            AddItemCmd addCmd = new AddItemCmd();


            var taskToSave = builder.BuildTask(formData);
            addCmd.ItemToWork = taskToSave;
            addCmd.AddItem();
            
            var tasks = SparkLogic.ReturnTaskList();
            String result = Razor.Resolve("GetAllTasks.cshtml", tasks).Run(new ExecuteContext());
            var res = Request.CreateResponse(HttpStatusCode.OK);
            Formatter.FormatResponse(res, result);
            return res;
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

            var tasks = SparkLogic.ReturnTaskList();
            String result = Razor.Resolve("GetAllTasks.cshtml", tasks).Run(new ExecuteContext());
            var res = Request.CreateResponse(HttpStatusCode.OK);
            Formatter.FormatResponse(res, result);
            return res;
        }
    }
}
