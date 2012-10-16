using ChronoSpark.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Logic
{
    public class SparkLogic
    {
        private static bool wasInitialized = false;
        
        public static bool Initialize()
        {
            if (wasInitialized) { return true; }

            Repository.RavenInitialize();
            wasInitialized = true;
            return true;
        }

        public static string ProcessCommand(string cmd)
        {
            return "The command was processed";
        }
    }
}
