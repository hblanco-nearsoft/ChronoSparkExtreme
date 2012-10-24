using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    class GetByIdCmd
    {
        
        Repository Repo;
        IRavenEntity ItemToGet;

        public GetByIdCmd() { }

        public GetByIdCmd(Repository receivedRepository, IRavenEntity receivedItem) 
        {

            Repo = receivedRepository;
            ItemToGet = receivedItem;

        }

        public bool execute() 
        {

            Repo.GetById(ItemToGet);
            return true;

        }
    }
}
