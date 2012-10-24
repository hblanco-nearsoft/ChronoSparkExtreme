using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace ChronoSpark.Data.Entities.Tests
{
    [TestClass]
    public class SparkTaskTests 
    {
        [TestMethod]
        public void LoadString_TaskHasValidId_ReturnsId()
        {
            var newTask = new SparkTask 
            {
                Id = "Correct Task Id"
            };

            newTask.LoadString().ShouldBe("Correct Task Id");
        }

        [TestMethod]
        public void TaskLoadString_TaskDoesntHaveId_ReturnsNoSuchId()
        {
            var noIdTask = new SparkTask();

            noIdTask.LoadString().ShouldBe("There's no ID");
        }

       
    }


    
}
