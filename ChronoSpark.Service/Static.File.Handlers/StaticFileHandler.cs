using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChronoSpark.Service.Static.File.Handlers
{
    public class StaticJSFileHandler : IHttpHandler
    {
        #region Privates

        private const string StaticFilesJSPhysicalDirectory = "~/Scripts/";

        private const string JavaScriptContentType = "application/javascript";

        private string filename;

        #endregion


        public StaticJSFileHandler(string filename)
        {
            this.filename = filename;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();

            string filepath = context.Server.MapPath(Path.Combine(StaticFilesJSPhysicalDirectory, filename));

            if (!System.IO.File.Exists(filepath))
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            else
            {
                context.Response.ContentType = JavaScriptContentType;
                context.Response.WriteFile(filepath);
            }

            context.Response.End();
        }
    }
}
