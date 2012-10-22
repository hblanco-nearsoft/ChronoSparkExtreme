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
        public String Id { get; set; }
        public String Description { get; set; }
        public int Interval { get; set; } // in minutes
        public String OwnerTask { get; set; }
        

        //public String LoadString() 
        //{ 
        //    if(Id.IsNotNullOrEmpty())
        //    {
        //        return Id;
        //    }
        //    return "There's no ID";
        //}
        
        //public bool Validate()
        //{
           
        //    if (Id != null && Description.IsNotNullOrEmpty() && Interval > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //public bool ValidateToAdd() 
        //{
        //    if (Description.IsNotNullOrEmpty() && Interval > 0) 
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        
    }
}
