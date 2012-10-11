using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Data.Entities
{
    public class Task
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Startdate { get; set; }

        //How are we going to messure the duration of a task?
        //How are we going to track who is editing this task?
        //Do you think we are going to be needing somekind of permissions schema?
    }
}
