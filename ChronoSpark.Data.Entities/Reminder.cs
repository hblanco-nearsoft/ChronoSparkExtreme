using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Common;

namespace ChronoSpark.Data.Entities
{
    public enum ReminderType
    {
        User,
        System
    }

    public class Reminder : IRavenEntity 
    {
        public String Id { get; set; }
        public String Description { get; set; }
        public int Interval { get; set; } // in minutes
        public ReminderType Type { get; set; }
        public DateTime TimeOfActivation { get; set; }
    }
}
