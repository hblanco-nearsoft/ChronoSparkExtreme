using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    public class GetByIdCmd : ICommand, ICommandFactory
    {
        
        IRepository Repo;
        public IRavenEntity ItemToWork { get; set; }

        public GetByIdCmd(IRepository receivedRepository) 
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

            Repo.GetById(ItemToWork);
            return true;

        }

        public String CommandName { get { return "getbyid"; } }
        public String CommandDescription { get { return "getbyid id"; } }

        public ICommand MakeCommand()
        {
            return new GetByIdCmd(Repo);
        }
    }
}
