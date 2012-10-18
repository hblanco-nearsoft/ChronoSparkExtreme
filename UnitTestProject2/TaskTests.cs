using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace ChronoSpark.Data.Entities.Tests
{
    [TestClass]
    public class TaskTests 
    {
        [TestMethod]
        public void LoadString_TaskHasValidId_ReturnsId()
        {
            var newTask = new Task 
            {
                Id = "Correct Task Id"
            };

            newTask.LoadString().ShouldBe("Correct Task Id");
        }

        [TestMethod]
        public void TaskLoadString_TaskDoesntHaveId_ReturnsNoSuchId()
        {
            var noIdTask = new Task();

            noIdTask.LoadString().ShouldBe("there's no ID");
        }

        [TestMethod]
        public void TaskValidateToAdd_TaskWithDescription_ReturnsTrue() 
        {
            var validTask = new Task 
            {
                Description = "Valid Description"
            };

            validTask.ValidateToAdd().ShouldBe(true);
        }

        [TestMethod]
        public void TaskValidateToAdd_TashWithoutDescription_ReturnsFalse() 
        {
            var invalidTask = new Task();
            invalidTask.ValidateToAdd().ShouldBe(false);
        }

        [TestMethod]
        public void TaskValidate_TaskWithValidIdDescriptionAndDuration_ReturnsTrue() 
        {
            var validTask = new Task
            {
                Id = "Valid Id",
                Description = "Valid Description",
                Duration = 5
            };

            validTask.Validate().ShouldBe(true);
        }

        [TestMethod]
        public void TaskValidate_TaskWithoutId_ReturnsFalse() 
        {
            var noIdTask = new Task 
            {
                Description = "Valid Description",
                Duration = 5
            };

            noIdTask.Validate().ShouldBe(false);

        }

        [TestMethod]
        public void TaskValidate_TaskWithoutDescription_ReturnsFalse() 
        {
            var DescriptionlessTask = new Task 
            {
                Id = "Valid Id",
                Duration = 5
            };

            DescriptionlessTask.Validate().ShouldBe(false);
        }

        [TestMethod]
        public void TaskValidate_TaskWithoutDuration_ReturnsFalse() 
        {
            var DurationlessTask = new Task
            {
                Id = "Valid Id",
                Description = "Valid Description"
            };

            DurationlessTask.Validate().ShouldBe(false);
        }

    }
}
