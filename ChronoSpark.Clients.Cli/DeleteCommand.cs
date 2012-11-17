﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManyConsole;
using NDesk.Options;
using ChronoSpark.Logic;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Clients.Cli
{
    class DeleteCommand : ConsoleCommand
    {
        public DeleteCommand() 
        {
            this.IsCommand("Delete", "Deletes the selected item");
            this.HasRequiredOption("e|EntityType=", "The type of the entity you wnat to delete", e => EntityType = e);
            this.HasRequiredOption("id=", "the number of Id of the item you wnat to delete", i => IdToDelete = i);
        
        }
        public String IdToDelete;
        public String EntityType;

        public override int Run(string[] remainingArguments) 
        {

            if (EntityType.ToLower() == "task")
            {
                SparkTask taskToDelete = new SparkTask();
                var actualId = "SparkTasks/" + IdToDelete;
                taskToDelete.Id = actualId;

                var availableCommands = SparkLogic.GetAvailableCommands();
                var parser = new CommandParser(availableCommands);
                var theCommand = parser.ParseCommand("delete");
                theCommand.ItemToWork = taskToDelete;
                var result = SparkLogic.ProcessCommand(theCommand);
                Console.WriteLine(result);
                return 0;
            }

            if(EntityType.ToLower() == "reminder")
            {
                SparkTask taskToDelete = new SparkTask();
                var actualId = "Reminders/" + IdToDelete;
                taskToDelete.Id = actualId;

                var availableCommands = SparkLogic.GetAvailableCommands();
                var parser = new CommandParser(availableCommands);
                var theCommand = parser.ParseCommand("delete");
                theCommand.ItemToWork = taskToDelete;
                var result = SparkLogic.ProcessCommand(theCommand);
                Console.WriteLine(result);
                return 0;
            }
            else { Console.WriteLine("The type of entity should be a task or reminder"); }
            return 0;
        }
    }

   
}