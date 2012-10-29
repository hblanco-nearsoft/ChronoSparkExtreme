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

        public GetByIdCmd() { }

        public GetByIdCmd(IRepository receivedRepository, IRavenEntity receivedItem) 
        {

            Repo = receivedRepository;
            ItemToGet = receivedItem;

        }

        public bool Execute() 
        {

            Repo.GetById(ItemToGet);
            return true;

        }

        public String CommandName { get { return "getbyid"; } }
        public String CommandDescription { get { return "getbyid id"; } }

    }
}
