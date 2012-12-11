using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;


namespace ChronoSpark.Logic
{
    public class DeleteItemCmd
    {

        IRepository Repo = new Repository();
        public IRavenEntity ItemToWork {get; set;}

        public String DeleteItem() 
        {
            if (Repo.Delete(ItemToWork))
            {
                return "The item has been deleted";
            }
            return "The item could not be deleted";
        }

    }
}
