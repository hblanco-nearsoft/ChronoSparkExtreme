using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public class ListReminderCmd
    {

        IRepository repo;
        public ListReminderCmd(IRepository receivedRepo) 
        {
            repo = receivedRepo;
        }
        public IRavenEntity ItemToWork { get; set; }


        public IEnumerable<Reminder> GetList() 
        {
            var listOfReminders = repo.GetReminderList();
            return listOfReminders;
        }
    }
}
