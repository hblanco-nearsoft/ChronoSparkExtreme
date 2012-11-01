using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    class AddReminderCmd: ICommand, ICommandFactory
    {
        IRepository Repo;
        Reminder ItemToAdd = new Reminder();

        public AddReminderCmd(IRepository receivedRepository)
        {

            Repo = receivedRepository;
                 
        }

        public bool Execute() 
        {
            while (ItemToAdd.Description == null)
            {
                Console.WriteLine("Add a description for the reminder");
                ItemToAdd.Description = Console.ReadLine();
            }
            while (ItemToAdd.Interval == 0)
            {
                Console.WriteLine("Add an interval (in minutes) for the reminder");
                ItemToAdd.Interval = int.Parse(Console.ReadLine());
            }
            Repo.Add(ItemToAdd);
            Console.WriteLine("Reminder saved");
            return true;
        }

        public String CommandName { get { return "add reminder"; } }
        public String CommandDescription { get { return "add reminder"; } }

        public ICommand MakeCommand() 
        {
            return new AddTaskCmd(Repo);
        }
    }
}
