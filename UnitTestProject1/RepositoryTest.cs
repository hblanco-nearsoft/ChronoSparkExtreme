﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;
using Raven.Client.Document;
using Raven.Client.Connection;
using Raven.Client.Embedded;
using Shouldly;

namespace UnitTestProject1
{
    [TestClass]
    public class RepositoryTests
    {
        private Repository _repo;

        // according to https://groups.google.com/forum/?fromgroups=#!topic/ravendb/M6TGgs9O4M8 
        //to clear when running in memory we create a new databasedocument would using it as testinitialize 
        //do that?
        //another source http://blog.orangelightning.co.uk/?p=105 suggest the use of a reset command which I can't find
        //[TestInitialize]     
        //public void Initialize()
        //{
        //    var _repo = new Repository(new EmbeddableDocumentStore()
        //    {
        //        DataDirectory = "~/Data/Debug",
        //        RunInMemory = true
        //    });
        //}

        //[TestCleanup]
        //public void Cleanup_DeleteAllRecords()
        //{

        //} commented while checking a couple more tests.
        
        [TestMethod]
        public void RepostoryAdd_AddsANewValidItem_ReturnsTrueOnSuccess()
        {
            var newTask = new Task
            {
                Description = "This is a Test",
            };
            var newReminder = new Reminder()
            {
                Description = "This is a Reminder",
                Interval = 5
            };
              
            var repo = new Repository(new EmbeddableDocumentStore()
            { 
                DataDirectory = "~/Data/Debug", 
                RunInMemory = true
            });

            repo.Add<Task>(newTask).ShouldBe(true);
            repo.Add<Reminder>(newReminder).ShouldBe(true);
        }

        [TestMethod]
        public void RepostoryAdd_AddsANewInvalidItem_ReturnsFalseOnFailure()
        {
            var descriptionlessTask = new Task
            {
                Description = "",
                Duration = 5

            };
            var descriptionlessReminder = new Reminder 
            { 
                Description = "",
                Interval = 5
            };

            var noIntervalReminder = new Reminder 
            {
                Description = "valid description",
                Interval = 0
            };

            var repo = new Repository(new EmbeddableDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true 
            });

            repo.Add<Task>(descriptionlessTask).ShouldBe(false);
            repo.Add<Reminder>(descriptionlessReminder).ShouldBe(false);
            repo.Add<Reminder>(noIntervalReminder).ShouldBe(false);
        }

        [TestMethod]
        public void RepositoryGetByID_IsGivenAValidId_ReturnsATask()
        {
            var newTask = new Task
            {
                Id = Guid.NewGuid().ToString(),
                Description = "Hello"
            };

            var repo = new Repository(new EmbeddableDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });
            repo.Add<Task>(newTask);

            var stored = repo.GetById<Task>(newTask);
            
            stored.ShouldBeTypeOf<Task>();
            stored.ShouldNotBe(null);
            stored.Id.ShouldBe(newTask.Id);
            stored.Description.ShouldBe("Hello");
        }
        
        [TestMethod] 
        public void RepositoryGetByID_IsGivenNullItem_ReturnsDefault() 
        {
            var nullTask = new Task();
            var repo = new Repository(new EmbeddableDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });
             
            repo.GetById<Task>(nullTask).ShouldBe(null);
        }


        [TestMethod]
        public void RepositoryUpdate_IsGivenExistingId_ReturnsTrue() 
        {
            var storedTask = new Task 
            {
                Id = Guid.NewGuid().ToString(),
                Description = "New Task",
                Duration = 5
            };
            var storedReminder = new Reminder 
            { 
                Id =  Guid.NewGuid().ToString(),
                Description = "New Reminder",
                Interval = 5
            };

            var repo = new Repository(new EmbeddableDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });

            repo.Add<Task>(storedTask);
            repo.Add<Reminder>(storedReminder);

            storedTask.Description = "Changed Task";
            storedReminder.Description = "Changed Reminder";

            
            repo.Update<Task>(storedTask).ShouldBe(true);
            repo.Update<Reminder>(storedReminder).ShouldBe(true);

           // repo.GetById<Task>(storedTask).Description.ShouldBe("Changed Task");
           // repo.GetById<Reminder>(storedReminder).Description.ShouldBe("Changed Reminder");
        }


        [TestMethod]
        public void RepositoryUpdate_IsGivenInvalidItem_ReturnsFalse()
        {
            var storedTask = new Task
            {
                Id = Guid.NewGuid().ToString(),
                Description = "New Task"
            };
            var storedReminder = new Reminder 
            {
                Id = Guid.NewGuid().ToString(),
                Description = "",
                Interval = 0
            };

            var repo = new Repository(new EmbeddableDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });

            repo.Add<Task>(storedTask);
            repo.Add<Reminder>(storedReminder);

            storedTask.Description = "Changed Task";
            storedReminder.Description = "Changed Reminder";


            repo.Update<Task>(storedTask).ShouldBe(false);
            repo.Update<Reminder>(storedReminder).ShouldBe(false);

            // repo.GetById<Task>(storedTask).Description.ShouldBe("Changed Task");
            // repo.GetById<Reminder>(storedReminder).Description.ShouldBe("Changed Reminder");
        }
    }
}
