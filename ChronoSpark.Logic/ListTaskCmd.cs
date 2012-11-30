using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public class ListTaskCmd : ICommand, ICommandFactory
    {

        IRepository repo;
        public ListTaskCmd(IRepository receivedRepo) 
        {
           repo = receivedRepo;
        }
        public IRavenEntity ItemToWork { get; set; }
        public bool Execute() 
        {
            var listOfTasks = repo.GetTaskList();
            return true;
        }

        public IEnumerable<SparkTask> GetList() 
        {
            var listOfTasks = repo.GetTaskList();
            return listOfTasks;
        }

        public String CommandName { get { return "list"; } }
        public String CommandDescription { get { return "list"; } }

        public ICommand MakeCommand() 
        {
            return new ListTaskCmd(repo);
        }
    }
}
