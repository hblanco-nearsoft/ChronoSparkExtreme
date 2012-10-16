using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Common;

namespace ChronoSpark.Data.Entities
{
    public class Reminder : IRavenEntity 
    {
        public String ID { get; set; }
        public String Description {get; set;}
        public int Interval {get; set;} // in minutes
        

        public String LoadString() 
        { 
            if(ID.IsNotNullOrEmpty())
            {
                return ID;
            }
            return "There's no ID";
        }
        
        public bool Validate()
        {
            
            if (ID != null && Description.IsNotNullOrEmpty() && Interval > 0)
            {
                return true;
            }
            return false;
        }
        
    }
}
