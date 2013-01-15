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

namespace ChronoSpark.Service
{

    public class HomeController : ApiController
    {
        [System.Web.Http.HttpGet]
        public HttpContent Index()
        {
            var model = new { Name = "World", Email = "someone@somewhere.com" };
            string result = Razor.Resolve("Index.cshtml", model).Run(new ExecuteContext());
            return new StringContent(result, System.Text.Encoding.UTF8, "text/html");
        }
        
        [System.Web.Http.Authorize]
        [System.Web.Http.HttpGet]
        public String Something()
        {
            var userName = User.Identity.Name;
            return userName;
        }

        public IEnumerable<SparkTask> AllTasks() 
        {
            var tasks = SparkLogic.ReturnTaskList();

            return tasks;
        }


        public SparkTask TaskByID(String id)
        {
            IRavenEntity entityToFetch = new SparkTask {Id = "SparkTasks/" + id};

            var task = SparkLogic.fetch(entityToFetch) as SparkTask;

            return task;
        }   
    }
}
