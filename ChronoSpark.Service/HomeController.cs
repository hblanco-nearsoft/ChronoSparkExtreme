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


        public SparkTask TaskByID(String id)
        {
            IRavenEntity entityToFetch = new SparkTask {Id = "SparkTasks/" + id};

            var task = SparkLogic.fetch(entityToFetch) as SparkTask;

            return task;
        }   
    }
}
