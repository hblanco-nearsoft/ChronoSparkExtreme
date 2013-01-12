using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RazorEngine;


namespace ChronoSpark.Service
{

    public class HomeController : ApiController
    {

        public HttpContent SayHello()
        {
            string template = "Hello @Model.Name! Welcome to Web API and Razor!";
            string result = Razor.Parse(template, new { Name = "World" });

            return new StringContent(result, System.Text.Encoding.UTF8, "text/html"); 
        }
    }
}
