﻿using System;
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


namespace ChronoSpark.Service
{

    public class HomeController : ApiController
    {

        ResponseFormatter Formatter = new ResponseFormatter();

        [System.Web.Http.HttpGet]
        public HttpResponseMessage Index()
        {
            var model = SparkLogic.ReturnTaskList();
            //string result = Razor.Resolve("Index.cshtml", model).Run(new ExecuteContext());
            //return new StringContent(result, System.Text.Encoding.UTF8, "text/html");
            string result = Razor.Resolve("Index.cshtml", model).Run(new ExecuteContext());
            var response = Request.CreateResponse(HttpStatusCode.OK);
            Formatter.FormatResponse(response,result);
            //res.Content = new StringContent(result, System.Text.Encoding.UTF8, "text/html");
            //res.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
        
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Something()
        {
            var userName = User.Identity;


            String result = Razor.Resolve("Something.cshtml", userName).Run(new ExecuteContext());
            var res = Request.CreateResponse(HttpStatusCode.OK);
            Formatter.FormatResponse(res, result);

            return res;

        }

    public IEnumerable<SparkTask> GetTasks()
        {             
             return SparkLogic.ReturnTaskList();   
        }

    [System.Web.Http.HttpPost]
    public HttpResponseMessage AddTask(SparkTask task) 
    {
        var addCmd = new AddItemCmd();

        //var taskToAdd = new SparkTask
        //{
        //    Description = description,
        //    Duration = duration,
        //    Client = client, 
        //    StartDate = DateTime.Now
        //};
        var taskToAdd = task;
        taskToAdd.StartDate = DateTime.Now;

        addCmd.ItemToWork = taskToAdd;
        addCmd.AddItem();

        return Request.CreateResponse<SparkTask>(HttpStatusCode.OK, task);
    }

    [System.Web.Http.HttpPost]
    public HttpResponseMessage SaveChanges(SparkTask formData)
    {
        SparkTaskBuilder builder = new SparkTaskBuilder();
        UpdateItemCmd updateCmd = new UpdateItemCmd();

        var taskToSave = builder.RebuildTask(formData);
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


    }
}
