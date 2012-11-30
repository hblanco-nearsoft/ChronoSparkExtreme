using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;


namespace ChronoSpark.Data
{
    public interface IRepository 
    {
        bool Initialize();
        bool Add<T>(T task) where T : class, IRavenEntity;
        bool Update<T>(T task) where T : class, IRavenEntity;
        bool Delete<T>(T task) where T : class, IRavenEntity;
        T GetById<T>(T item) where T : class, IRavenEntity;
        IEnumerable<SparkTask> GetTaskList();
        IEnumerable<Reminder> GetReminderList();
    }
}