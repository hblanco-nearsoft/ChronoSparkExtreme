using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;
using ChronoSpark.Logic;
using Shouldly;

namespace ChronoSpark.Logic.Tests
{
    [TestClass]
    public class SparkLogicTests
    {   
        [TestMethod]
        public void ProcessCommand_ReceivesValidCommand_ReturnsProcessed()
        {
            var repo = new Repository(new ChronoDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });

            ICommand theCommand = new AddItemCmd(repo);
            IRavenEntity entity = new SparkTask{
            Description = "task",
            Duration = 5
            };

            theCommand.ItemToWork = entity;
            var result = SparkLogic.ProcessCommand(theCommand);

            result.ShouldBe("The command was executed");

        }

        [TestMethod]
        public void ProcessCommand_ReceiveNullCommand_ReturnsInvalid() 
        {

            var repo = new Repository(new ChronoDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });


            var result = SparkLogic.ProcessCommand(null);

            result.ShouldBe("Unidentified command");
        }
    }
}
