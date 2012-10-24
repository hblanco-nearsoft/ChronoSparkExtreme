using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;


namespace ChronoSpark.Logic
{
    class DeleteItemCmd : ICommand
    {

        Repository Repo;
        IRavenEntity ItemToDelete;

        public DeleteItemCmd() { }

        public DeleteItemCmd(Repository receivedRepository, IRavenEntity receivedItem) 
        {

            Repo = receivedRepository;
            ItemToDelete = receivedItem;

        }

        public bool Execute() 
        {
            Repo.Delete(ItemToDelete);
            return true;
        }

        
    }
}
