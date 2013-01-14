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
using System.Collections.Generic;
using RazorEngine.Templating;

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
        
        [System.Web.Http.HttpGet]
        public String Something()
        {
            var userName = User.Identity.Name;
            return userName;

        }
    }
}
