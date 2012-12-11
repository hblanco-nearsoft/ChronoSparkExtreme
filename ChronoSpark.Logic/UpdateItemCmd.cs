using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public class UpdateItemCmd
    {

        private IRepository repo = new Repository();
        public IRavenEntity ItemToWork { get; set; }

        public String UpdateItem()
        {
            if (repo.Update(ItemToWork)) { return "The item has been updated."; }
            return "The item could not be updated.";
        }
    }
}
