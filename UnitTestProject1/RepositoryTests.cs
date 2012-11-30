using System;
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

        // according to https://groups.google.com/forum/?fromgroups=#!topic/ravendb/M6TGgs9O4M8 
        //to clear when running in memory we create a new databasedocument. would using it as testinitialize 
        //do that?
        //another source http://blog.orangelightning.co.uk/?p=105 suggest the use of a reset 
        //command which I can't seem to find
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

        //}   //commented while checking a couple more tests.
        
        [TestMethod]
        public void RepostoryAdd_AddsANewValidItem_ReturnsTrueOnSuccess()
        {
            var newTask = new SparkTask
            {
                Description = "This is a Test",
                Duration = 5
            };
            var newReminder = new Reminder()
            {
                Description = "This is a Reminder",
                Interval = 5
            };
              
            var repo = new Repository(new ChronoDocumentStore()
            { 
                DataDirectory = "~/Data/Debug", 
                RunInMemory = true
            });

            repo.Add<SparkTask>(newTask).ShouldBe(true);
            repo.Add<Reminder>(newReminder).ShouldBe(true);
        }

        [TestMethod]
        public void RepostoryAdd_AddsANewInvalidItem_ReturnsFalseOnFailure()
        {
            var descriptionlessTask = new SparkTask
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

            var repo = new Repository(new ChronoDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true 
            });

            repo.Add<SparkTask>(descriptionlessTask).ShouldBe(false);
            repo.Add<Reminder>(descriptionlessReminder).ShouldBe(false);
            repo.Add<Reminder>(noIntervalReminder).ShouldBe(false);
        }

        [TestMethod]
        public void RepositoryGetByID_IsGivenAValidId_ReturnsATask()
        {
            var newTask = new SparkTask
            {
                Id = Guid.NewGuid().ToString(),
                Description = "Hello",
                Duration = 5
            };

            var repo = new Repository(new ChronoDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });
            repo.Add<SparkTask>(newTask);

            var stored = repo.GetById<SparkTask>(newTask);
            
            stored.ShouldBeTypeOf<SparkTask>();
            stored.ShouldNotBe(null);
            stored.Id.ShouldBe(newTask.Id);
            stored.Description.ShouldBe("Hello");
        }
        
        [TestMethod] 
        public void RepositoryGetByID_IsGivenNullItem_ReturnsDefault() 
        {
            var repo = new Repository(new ChronoDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });

            repo.GetById<SparkTask>(null).ShouldBe(null);
        }

        [TestMethod]
        public void RepositoryGetById_IsGivenNonExistingItem_ReturnsDefault()
        {

            var nonExistingTask = new SparkTask 
            {
                Id = Guid.NewGuid().ToString(),
                Description = "this doesn't exist in the database",
                Duration = 7
            };

            var repo = new Repository(new ChronoDocumentStore
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });

            repo.GetById<SparkTask>(nonExistingTask).ShouldBe(null);
        }

        [TestMethod]
        public void RepositoryUpdate_IsGivenExistingId_ReturnsTrue() 
        {
            var storedTask = new SparkTask 
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

            var repo = new Repository(new ChronoDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });

            repo.Add<SparkTask>(storedTask);
            repo.Add<Reminder>(storedReminder);

            storedTask.Description = "Changed Task";
            storedReminder.Description = "Changed Reminder";

            
            repo.Update<SparkTask>(storedTask).ShouldBe(true);
            repo.Update<Reminder>(storedReminder).ShouldBe(true);

        }


        [TestMethod]
        public void RepositoryUpdate_IsGivenInvalidItem_ReturnsFalse()
        {
            var nonExistingTask = new SparkTask
            {
                Id = Guid.NewGuid().ToString(),
                Description = "New Task",
                Duration = 5
            };
            var nonExistingReminder = new Reminder 
            {
                Id = Guid.NewGuid().ToString(),
                Description = "New Reminder",
                Interval = 5
            };
            var repo = new Repository(new ChronoDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });
            
            repo.Update<Reminder>(nonExistingReminder).ShouldBe(false);
            repo.Update<SparkTask>(nonExistingTask).ShouldBe(false);
        }

        [TestMethod]
        public void RepositoryDelete_IsGivenAnExistingId_ItemIsDeleted() 
        {
            var newTask = new SparkTask
            {
                Id = Guid.NewGuid().ToString(),
                Description = "I want to delete this",
                Duration = 6
            };

            var repo = new Repository(new ChronoDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });

            repo.Add<SparkTask>(newTask);

            var checkTask = repo.GetById<SparkTask>(newTask);
            checkTask.Id.ShouldBe(newTask.Id);
            checkTask.Description.ShouldBe(newTask.Description);
            checkTask.Duration.ShouldBe(newTask.Duration);

            repo.Delete<SparkTask>(newTask).ShouldBe(true);

            repo.GetById<SparkTask>(newTask).ShouldBe(null);
        }

        [TestMethod]
        public void RepositoryDelete_IsGivenInvalidItem_ReturnsFalse() 
        {
            var invalidTask = new SparkTask {
            Description = "",
            Id = ""
            };
            var repo = new Repository(new ChronoDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });

            repo.Delete<SparkTask>(invalidTask).ShouldBe(false);
        }

        [TestMethod]
        public void RepositoryDelete_IsGivenNonExistentItem_ReturnsFalse() 
        {
            var nonExistentTask = new SparkTask
            {
                Id = Guid.NewGuid().ToString(),
                Description = "I don't exist",
                Duration = 6
            };
            var repo = new Repository(new ChronoDocumentStore()
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            });

            repo.Delete<SparkTask>(nonExistentTask).ShouldBe(false);
        }

        [TestMethod]
        public void ChronoDataStore()
        {
            var docStore = new ChronoDocumentStore
            {
                DataDirectory = "~/Data/Debug",
                RunInMemory = true
            };

            docStore.Initialize();

            var task = new SparkTask();

            using (var session = docStore.OpenSession())
            {
                session.Store(task);

                Should.Throw<ArgumentNullException>(session.SaveChanges);
            }
        }
    }
}