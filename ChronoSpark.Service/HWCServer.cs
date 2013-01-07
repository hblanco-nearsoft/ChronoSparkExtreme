using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ChronoSpark.Service
{
    public class HWCServer : IDisposable
    {  
        private string _appHostConfigPath;
        private string _rootWebConfigPath;

        public HWCServer(string physicalPath, int port, int siteId) {
            string appPoolName = "AppPool" + port.ToString();
            _appHostConfigPath = Path.Combine(Path.GetTempPath(), Path.GetTempFileName() + ".config");
            _rootWebConfigPath = Environment.ExpandEnvironmentVariables(@"%windir%\Microsoft.Net\Framework\v4.0.30319\config\web.config");

            string appHostContent = Resources.AppHostAspNet;
            //string appHostContent = Resources.AppHostStaticFiles;

            File.WriteAllText(_appHostConfigPath,
                String.Format(appHostContent,
                              port,
                              physicalPath,
                              @"%windir%\Microsoft.NET\Framework\v4.0.30319",
                              siteId,
                              appPoolName));
        }

        ~HWCServer() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
        }

        private void Dispose(bool disposing) {
            if (disposing) {
                GC.SuppressFinalize(this);
            }

            Stop();
        }

        public void Start() {
            if (!HostableWebCore.IsActivated) {
                HostableWebCore.Activate(_appHostConfigPath, _rootWebConfigPath, Guid.NewGuid().ToString());
            }
        }

        public void Stop() {
            if (HostableWebCore.IsActivated) {
                HostableWebCore.Shutdown(false);
            }
        }
    }
}
