using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChronoSpark.Common;

namespace ChronoSpark.Data.Entities
{
    public enum TaskState
    {
        InProgress,
        Finished
    }
    
    public class Task : IRavenEntity      
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Client { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; } //In minutes
        public TaskState State { get; set; }
        public String LastEditedBy { get; set; }
  
        public bool Validate()
        {
            
            if (ID != null && Description.IsNotNullOrEmpty() && Duration > 0)
            {
                return true;
            }
            return false;
        }
            
  

        public string LoadString()
        {
            return ID; //What if this is Empty? What if I call this BEFORE having an ID?
        }
    }
}
