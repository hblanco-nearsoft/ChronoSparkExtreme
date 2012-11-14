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
       public IRavenEntity ItemToWork { get; set; }


        public UpdateItemCmd(IRepository receivedRepository) 
        {

            Repo = receivedRepository; 

        }


        public bool Execute()
        {

            Repo.Update(ItemToWork);
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
