using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public class UpdateItemCmd : ICommand, ICommandFactory  
    {

       IRepository Repo;
       IRavenEntity ItemUpdated;


        public UpdateItemCmd(IRepository receivedRepository) 
        {

            Repo = receivedRepository; 

        }

        public bool SetEntity(IRavenEntity receivedEntity)
        {
            ItemUpdated = receivedEntity;
            return true;
        }

        public bool Execute()
        {

            Repo.Update(ItemUpdated);
            return true;

        }

        public String CommandName { get { return "update"; } }
        public String CommandDescription { get { return "update itemtosave"; } }

        public ICommand MakeCommand()
        {
            return new UpdateItemCmd(Repo);
        }
    }
}
