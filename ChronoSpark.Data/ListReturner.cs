using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Data
{
    //public class YoDawg
    public class ListReturner
    {
        public static IEnumerable<SparkTask> ReturnList()
        {
            Repository repo = new Repository();
            var listToReturn = repo.GetTaskList();
            return listToReturn;
        }
    }
}
