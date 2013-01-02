using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace ChronoSpark.Service
{
    public class ServiceListener
    {
        public void ActivateListener() 
        {
            HttpListener listener = null;
                
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/chronospark/");
            listener.Start();

            while (true) 
            {
                HttpListenerContext context = listener.GetContext();
                String msg = "<html>Chrono Spark!</html>";
                context.Response.ContentLength64 = Encoding.UTF8.GetByteCount(msg);
                context.Response.StatusCode = (int)HttpStatusCode.OK;

                using (Stream stream = context.Response.OutputStream) 
                {
                    using (StreamWriter streamWriter = new StreamWriter(stream)) 
                    {
                        streamWriter.Write(msg);
                    }
                }
            }                
        }
    }
}
