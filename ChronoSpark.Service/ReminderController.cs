using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RazorEngine;
using System.Web.Mvc;
using ChronoSpark.Data.Entities;
using ChronoSpark.Logic;
using RazorEngine.Templating;
using ChronoSpark.Data;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;


namespace ChronoSpark.Service
{
    public class ReminderController : ApiController
    {

        public IEnumerable<Reminder> GetReminders() 
        {
            return SparkLogic.ReturnReminderList();
        }

        public void Post(Reminder reminderToEdit) 
        {
            UpdateItemCmd updateCmd = new UpdateItemCmd();
            updateCmd.ItemToWork = reminderToEdit;
            updateCmd.UpdateItem();
        }

        public Reminder GetReminder(Reminder theReminder) 
        {
            var reminderToFetch = theReminder;
            var fetchedReminder = SparkLogic.fetch(reminderToFetch) as Reminder;

            return fetchedReminder;
        }

    }
}
