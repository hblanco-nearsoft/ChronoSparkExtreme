using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities; 

namespace ChronoSpark.Logic
{
    public class AddItemCmd    
    {

        public IRavenEntity ItemToWork { set; get; }
        private IRepository repo = new Repository();

        public String AddItem() 
        {
            if (repo.Add(ItemToWork))
            {
                Console.WriteLine("Item saved");
                return "The task has been saved";
            }
            return "The task could not be saved.";
        }

    }
}