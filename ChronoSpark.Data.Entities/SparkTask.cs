using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChronoSpark.Common;

namespace ChronoSpark.Data.Entities
{
    public enum TaskState
    {
        Paused,
        InProgress,     
        Finished,
        reported,
    }
    
    public class SparkTask : IRavenEntity      
    {
        public String Id { get; set; }
        public String Client { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; } //In minutes
        public TaskState State { get; set; }
        public String LastEditedBy { get; set; }
        public TimeSpan TimeElapsed { get; set; }

       
    }
}
