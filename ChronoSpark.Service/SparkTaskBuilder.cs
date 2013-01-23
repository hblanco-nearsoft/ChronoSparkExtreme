using ChronoSpark.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Service
{
    public class SparkTaskBuilder
    {

        public SparkTask BuildTask(FormDataCollection formData) 
        {
            var el1 = formData.ElementAt(0);
            var el2 = formData.ElementAt(1);
            int durationValue;

            if (!int.TryParse(el2.Value, out durationValue)) 
            {
                durationValue = 0;
            }


            var el3 = formData.ElementAt(2);

            SparkTask buildedTask = new SparkTask
            {
                Description = el1.Value,
                Duration = durationValue,
                Client = el3.Value,
                StartDate = DateTime.Now,
                State = TaskState.Paused,
            };

            return buildedTask;
        }
    }
}
