using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //How are we going to track who is editing this task?
        public bool SelfValidate()
        {
            if (ID != null) 
            {
                if (Description != null)
                {
                    if (Duration != null)
                    {
                        return true;
                    }
                }   
            }
                return false;
        }

        public string LoadString()
        {
            return ID;
        }
    }
}
