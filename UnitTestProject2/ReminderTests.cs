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
    }
}
