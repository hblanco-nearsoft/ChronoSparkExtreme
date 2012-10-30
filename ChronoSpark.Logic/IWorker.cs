using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    interface IWorker
    {

        IRavenEntity getItem();
        
    }
}
