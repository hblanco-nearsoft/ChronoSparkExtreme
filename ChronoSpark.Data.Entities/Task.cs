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
        Suspended,
        Finished
    }
    
    public class Task : IRavenEntity      
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Client { get; set; }
        public String Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; } //In minutes
        public TaskState State { get; set; }
        public String LastEditedBy { get; set; }
  
        //public bool Validate()
        //{
            
        //    if (Id != null && Description.IsNotNullOrEmpty() && Duration > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public bool ValidateToAdd(){
        //    if(Description.IsNotNullOrEmpty())
        //    {
        //        return true;
        //    }
        //    return false;
        //}
            
  

        //public string LoadString()
        //{
        //    if (Id.IsNotNullOrEmpty())
        //    {
        //        return Id; //What if this is Empty? What if I call this BEFORE having an ID?
        //    }
        //    return "there's no ID";
        //}
    }
}
