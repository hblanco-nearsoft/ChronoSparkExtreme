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
        public IRavenEntity ItemToWork {get; set;}

        public DeleteItemCmd(IRepository receivedRepository) 
        {

            Repo = receivedRepository;

        }

        public bool SetEntity(IRavenEntity receivedEntity)
        {
            ItemToWork = receivedEntity;
            return true;
        }


        public bool Execute() 
        {
            Repo.Delete(ItemToWork);
            return true;
        }

        public String CommandName { get { return "delete"; } }
        public String CommandDescription { get { return "delete rules"; } }

        public ICommand MakeCommand()
        {
            return new DeleteItemCmd(Repo);
        }
    }
}
