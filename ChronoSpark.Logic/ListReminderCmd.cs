using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public class ListReminderCmd : ICommand, ICommandFactory
    {

        IRepository repo;
        public ListReminderCmd(IRepository receivedRepo) 
        {
           repo = receivedRepo;
        }
        public IRavenEntity ItemToWork { get; set; }
        public bool Execute() 
        {
            var listOfReminders = repo.GetReminderList();
            return true;
        }

        public IEnumerable<Reminder> GetList() 
        {
            var listOfReminders = repo.GetReminderList();
            return listOfReminders;
        }

        public String CommandName { get { return "list"; } }
        public String CommandDescription { get { return "list"; } }

        public ICommand MakeCommand() 
        {
            return new ListTaskCmd(repo);
        }
    }
}
