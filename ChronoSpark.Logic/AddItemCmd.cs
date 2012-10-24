using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities; 

namespace ChronoSpark.Logic
{
    class AddItemCmd : ICommand
    {

        Repository Repo;
        IRavenEntity ItemToAdd;

        public AddItemCmd() { }

        public AddItemCmd(Repository receivedRepo, IRavenEntity receivedItem)
        {

            ItemToAdd = receivedItem;
            Repo = receivedRepo;
                 
        }

        public bool Execute() 
        {

            Repo.Add(ItemToAdd);
            return true;
 
        }

    }
}
   