using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace ChronoSpark.Service.Static.File.Handlers
{
    public class StaticJSRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var filename = requestContext.RouteData.Values["filename"] as string;

            return new StaticJSFileHandler(filename);
        }
    }
}
