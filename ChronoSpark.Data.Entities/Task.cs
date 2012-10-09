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
        public string name { get; set; }
        public string description { get; set; }
        public DateTime Date { get; set; }
    }
}
