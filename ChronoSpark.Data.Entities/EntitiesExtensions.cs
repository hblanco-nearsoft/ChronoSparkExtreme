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
            else
            { return "There's no ID"; }
        }

    }
}
