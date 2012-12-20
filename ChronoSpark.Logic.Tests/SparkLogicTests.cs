using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;
using ChronoSpark.Logic;
using Shouldly;
using System.Collections.Generic;

namespace ChronoSpark.Logic.Tests
{
    [TestClass]
    public class SparkLogicTests
    {
        

        //[TestMethod]
        //public void Fetch_IsGivenValidExistingEntityToFetch_ReturnTheEntity()
        //{
        //    SparkTask theTask = new SparkTask()
        //    {
        //        Description = "dddd",
        //        Duration = 5,
        //        State = TaskState.Paused,
        //        Id = "theid"
        //    };
        //    IRepository repo = new Repository();
        //    Repository.RavenInitialize();
        //    repo.Add(theTask);
            
        //    var returnedTask = SparkLogic.fetch(theTask);

        //    returnedTask.ShouldBeSameAs(theTask);
        //}

        //[TestMethod]
        //public void Fetch_IsGivenValidNonExistingEntityToFetch_ReturnsNull()
        //{
        //    SparkTask theTask = new SparkTask()
        //    {
        //        Description = "dddd",
        //        Duration = 5,
        //        State = TaskState.Paused,
        //        Id = "theid"
        //    };
        //    var returnedTask = SparkLogic.fetch(theTask);

        //    returnedTask.ShouldBe(null);
        //}

        //[TestMethod]
        //public void ReturnTaskList_ThereIsAListToReturn_RetrunsTheList()
        //{
        //    var taskList = SparkLogic.ReturnTaskList();
        //}

    }
}
