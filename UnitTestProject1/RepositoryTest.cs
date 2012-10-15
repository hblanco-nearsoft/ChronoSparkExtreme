using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

using Shouldly;

namespace UnitTestProject1
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void RepostoryAdd_AddsANewItem_ReturnsTrueOnSuccess()
        {
            //Missing: 1. A way to configure the Repository

            var newTask = new Task();

            Repository.Instance.Add<Task>(newTask);

            var storedTasks = Repository.Instance.GetById<Task>(newTask);

            storedTasks.Name.ShouldBe("My Important Task");
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
