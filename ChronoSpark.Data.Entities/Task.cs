using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Data.Entities
{
    public enum TaskState
    {

    }

    public interface IRavenEntity
    {
        String ID { get; set; }
        String LoadString();
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
        
        
        //How are we going to messure the duration of a task?
        //How are we going to track who is editing this task?
        //Do you think we are going to be needing somekind of permissions schema?

        public string LoadString()
        {
            return "Task.ID";
        }
    }
}
