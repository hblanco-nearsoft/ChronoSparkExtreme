using ChronoSpark.Data;
using ChronoSpark.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ChronoSpark.Logic
{
    public class SparkLogic
    {
        private static bool wasInitialized = false;
        public static bool Initialize()
        {
            if (wasInitialized) { return true; }

            Repository.RavenInitialize();
            wasInitialized = true;
            DefaultReminders.AddDefaultReminders();
            return true;
        }

        public static IEnumerable<SparkTask> ReturnTaskList() 
        {
            Repository repo = new Repository();
            ListTaskCmd listCommand = new ListTaskCmd(repo);
            var listToReturn = listCommand.GetList();
            return listToReturn;
        }

        public static IEnumerable<SparkTask> ReturnNotReportedList()
        {
            Repository repo = new Repository();
            var notReportedList = repo.GetNonReportedList();
            return notReportedList;
        }

        public static IEnumerable<Reminder> ReturnReminderList() 
        {
            Repository repo = new Repository();
            ListReminderCmd listCommand = new ListReminderCmd(repo);
            var listToReturn = listCommand.GetList();
            return listToReturn;
        }

        public SparkTask ReturnActiveTask() 
        {
            Repository repo = new Repository();
            var activeTaskToReturn = repo.GetActiveTask();
            return activeTaskToReturn;
        }

        public static IRavenEntity fetch(IRavenEntity entity)
        {
            Repository repo = new Repository();
            var entityToReturn = repo.GetById(entity);
            if (entityToReturn != null) { return entityToReturn; }
            else { return null; }
        }
    }
}