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
        [TestMethod]
        public void RepostoryAdd_AddsANewValidItem_ReturnsTrueOnSuccess()
        {
            var newTask = new Task
            {
                Name = "This is a Test",
                Duration = 5
            };
   //         var reminder = new Reminder();
              
            var repo = new Repository(new EmbeddableDocumentStore()
            { 
                DataDirectory = "~/Data/Debug", 
                RunInMemory = true
            });

            repo.Add<Task>(newTask);
//repo.Add<Reminder>(reminder);

            repo.GetById<Task>(newTask).Description.ShouldBe("This is a Test");
        }

        
    }

  /*  public class FakeRepo : Repository
    {
        public new bool Add<T>(T item)
        {
            return true;
        }

    }*/
}
