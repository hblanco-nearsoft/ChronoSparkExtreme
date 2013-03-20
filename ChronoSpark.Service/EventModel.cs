using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Service
{
    public enum EventType
    {

        IntervalPassed,
        NoActiveTask,
        EndOfWeek
    }

    public class EventModel
    {
        public EventType Type { get; set; }
        public SparkTask SourceTask { get; set; }
    }
}
