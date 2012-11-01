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
        IRavenEntity ItemToGet;

        public GetByIdCmd(IRepository receivedRepository) 
        {

            Repo = receivedRepository;

        }

        public bool SetEntity(IRavenEntity receivedEntity)
        {
            ItemToGet = receivedEntity;
            return true;
        }

        public bool Execute() 
        {

            Repo.GetById(ItemToGet);
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
