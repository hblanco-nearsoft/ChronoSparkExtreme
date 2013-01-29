using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using RazorEngine;
using System.Web.Mvc.Razor;
using System.Web.Mvc;
using System.Net.Http.Headers;
using RazorEngine.Templating;

namespace ChronoSpark.Service
{
    public class ResponseFormatter
    {

        public HttpResponseMessage FormatResponse(HttpResponseMessage res, String result) 
        {          
            res.Content = new StringContent(result, System.Text.Encoding.UTF8, "text/html");
            res.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return res;
        }
       

    }
}
