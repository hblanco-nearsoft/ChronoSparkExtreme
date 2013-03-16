using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Service.Models
{
    public class TaskModel : SparkTask
    {
        public String TimeInHours { get; set; }
    }
}
