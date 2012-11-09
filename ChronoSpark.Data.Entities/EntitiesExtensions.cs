using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Common;

namespace ChronoSpark.Data.Entities
{
    public static class EntitiesExtensions
    {
        public static String LoadString(this IRavenEntity entity) 
        {
            if (entity.Id.IsNotNullOrEmpty())
            {
                return entity.Id;
            }
            else { return "There's no ID"; }
        }

        public static String ElapsedTime(this SparkTask ent)
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan elapsedTime = currentTime - ent.StartDate;
            ent.TimeElapsed = ent.TimeElapsed + elapsedTime;
          //  return ent.TimeElapsed;
            return "";
        }

    }
}
