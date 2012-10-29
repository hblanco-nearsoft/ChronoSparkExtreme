using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;


namespace ChronoSpark.Logic
{
    public class DeleteItemCmd : ICommand, ICommandFactory
    {

        IRepository Repo;
        IRavenEntity ItemToDelete;

        public DeleteItemCmd() { }

        public DeleteItemCmd(IRepository receivedRepository, IRavenEntity receivedItem) 
        {

            Repo = receivedRepository;
            ItemToDelete = receivedItem;

        }

        public bool Execute() 
        {
            Repo.Delete(ItemToDelete);
            return true;
        }

        public String CommandName { get { return "delete"; } }
        public String CommandDescription { get { return "delete rules"; } }

        
    }
}
