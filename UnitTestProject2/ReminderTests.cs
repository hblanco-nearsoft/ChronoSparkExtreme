using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace ChronoSpark.Data.Entities.Tests
{
    [TestClass]
    public class ReminderTests
    {
        [TestMethod]
        public void ReminderLoadString_ReminderHadId_ReturnsId()
        {
            var identifiedReminder = new Reminder
            {
                Id = "Reminder"
            };

            identifiedReminder.LoadString().ShouldBe("Reminder");
        }

        [TestMethod]
        public void ReminderLoadString_ReminderWithNoId_ReturnsNoId() 
        {
            var unidentifiedReminder = new Reminder();
            unidentifiedReminder.LoadString().ShouldBe("There's no ID");
        }

    //    [TestMethod]
    //    public void ReminderValidateToAdd_ReminderIsValid_ReturnsTrue() 
    //    {
    //        var validReminder = new Reminder 
    //        {
    //            Description = "Valid Description",
    //            Interval = 5
    //        };

    //        validReminder.ValidateToAdd().ShouldBe(true);
    //    }

    //    [TestMethod]
    //    public void ReminderValidateToAdd_ReminderHasNoDescription_ReturnsFalse() 
    //    {
    //        var descriptionlessReminder = new Reminder 
    //        {
    //            Interval =5
    //        };

    //        descriptionlessReminder.ValidateToAdd().ShouldBe(false);
    //    }

    //    [TestMethod]
    //    public void ReminderValidateToAdd_ReminderHasNoInterval_ReturnsFalse()
    //    {
    //        var descriptionlessReminder = new Reminder
    //        {
    //            Description = "Valid Description"
    //        };

    //        descriptionlessReminder.ValidateToAdd().ShouldBe(false);
    //    }

    //    [TestMethod]
    //    public void ReminderValidate_ReminderHasValidIdDescriptionAndInterval_ReturnsTrue() 
    //    {
    //        var validReminder = new Reminder 
    //        {
    //            Id = "valid ID",
    //            Description = "Valid Description",
    //            Interval = 5
    //        };

    //        validReminder.Validate().ShouldBe(true);
    //    }

    //    [TestMethod]
    //    public void ReminderValidate_ReminderHasNoDescription_ReturnsFalse() 
    //    {
    //        var validReminder = new Reminder
    //        {
    //            Id = "valid ID",
    //            Interval = 5
    //        };

    //        validReminder.Validate().ShouldBe(false);
    //    }

    //    [TestMethod]
    //    public void ReminderValidate_ReminderHasNoId_ReturnsFalse()
    //    {
    //        var validReminder = new Reminder
    //        {
    //            Description = "Valid Description",
    //            Interval = 5
    //        };

    //        validReminder.Validate().ShouldBe(false);
    //    }

    //    [TestMethod]
    //    public void ReminderValidate_ReminderHasNoInterval_ReturnsFalse()
    //    {
    //        var validReminder = new Reminder
    //        {
    //            Id = "valid ID",
    //            Description = "Valid Description"
    //        };

    //        validReminder.Validate().ShouldBe(false);
    //    }
    }
}
