using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Document;
using Raven.Client.Connection;
using Raven.Client.Embedded;
using System.Runtime.InteropServices;
using Omu.ValueInjecter;
using Raven.Client;
using ChronoSpark.Data.Entities;
using Raven.Storage.Esent;
using Raven.Storage.Managed;
using ChronoSpark.Common;

namespace ChronoSpark.Data
{
    /*  General Comments
     *     Try to use descriptive names, like, if you are retrieving a document from the db, don't call it
     *     "task", that's the most generic name ever, called descriptibly: storedDocument, toDeleteTask, etc.,
     *     the idea is that your code can be EASILY read, like a sentence.
     *     
     * Oct. 15. Good Work! We are almost there with the data layer, need some unit testing though.
     */
    public class Repository : IRepository, IDisposable 
    {
        private DocumentStore DocStore;
        private bool Disposed = false;

        #region Constructors

        public Repository()
        {
        }

        public Repository(IDocumentStore AltDocStore)
        {
            _docStore = AltDocStore;
            _docStore.Initialize();
        }

        #endregion


  
        private static IDocumentStore _docStore;
        public static bool RavenInitialize()
        {
            _docStore = new ChronoDocumentStore()
            {
                ConnectionStringName = "RavenDB",
                RunInMemory = true
                //UseEmbeddedHttpServer = true
            };
            
            _docStore.Initialize();
          
            return true;
        }


    
        public bool Initialize()
        {
            _docStore = new EmbeddableDocumentStore()
            {
                ConnectionStringName = "RavenDB",
                UseEmbeddedHttpServer = true
            }; 

            /* The initialize operation is REALLY heavy on the things it does,
             * we need to find a way to make this call only ONCE in the whole
             * app life cycle.
             * */
            DocStore.Initialize(); 
            return true;
        }

   /*     public bool Dispose(bool Disposing)
        {
            Dispose(true);
            GC.SuppressFinalize(this);
             if (!Disposed)
            {
                    if (DocStore != null)
                    {
                         DocStore.Dispose();
                    }
                Disposed = true;
            }
            return true;
        }
        */
        public bool Add<T>(T newItem) where T : class, IRavenEntity
        {
            using (var Session = _docStore.OpenSession())
            {
                try
                {
                    Session.Store(newItem);
                    Session.SaveChanges();
                    return true;
                }
                catch (ArgumentNullException) { return false; }
                catch(IndexOutOfRangeException) { return false; }
                //catch (InvalidOperationException) { return false; }
            }  
        }

        //This Method is kind of cool! good work.
        public bool Update<T>(T UpdatedItem) where T : class, IRavenEntity
        {
            
            using (var Session = _docStore.OpenSession())
            {

                if (Session.Advanced.DatabaseCommands.Head(UpdatedItem.Id) == null) { return false; }
                    var doc = Session.Load<T>(UpdatedItem.LoadString());
                    doc.InjectFrom<InjectNotNull>(UpdatedItem);

                    try
                    {
                        Session.Store(doc);
                        Session.SaveChanges();
                        return true;
                    }
                    catch (ArgumentNullException) { return false; }
                    catch (IndexOutOfRangeException) { return false; }
                    //catch (InvalidOperationException) { return false; }
            } 
        }

        public bool Delete<T>(T toDeleteItem) where T : class, IRavenEntity
        {
            using (var session = _docStore.OpenSession())
            {
                /*
                 *  4. I don't know right now if we are going to have a special rule for deletion, but if
                 *     we decide so, we would need to also delete it.
                 */

                    if (session.Advanced.DatabaseCommands.Head(toDeleteItem.Id) == null) { return false; }
                    var doc = session.Load<T>(toDeleteItem.LoadString());
                    try
                    {
                        session.Delete(doc);
                        session.SaveChanges();
                        return true;
                    }
                    catch (ArgumentNullException) { return false; }
                    catch (IndexOutOfRangeException) { return false; }

            }
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
            if (!Disposed)
            {
                if (_docStore != null)
                {
                    _docStore.Dispose();
                }
                Disposed = true;
            }
            throw new NotImplementedException();
        }

        public T GetById<T>(T item) where T : class, IRavenEntity
        {
            if (item == null) { return default(T); }
            string myStr = String.Empty;
            using (var session = _docStore.OpenSession())
            {
                if (session.Advanced.DatabaseCommands.Head(item.Id) == null) { return default(T); }
                var storedItem = session.Load<T>(item.LoadString());
                return storedItem;
            }
        }

        public IEnumerable<SparkTask> GetTaskList()
        {
            using (var session = _docStore.OpenSession())
            {
                var queriedTasks = session.Query<SparkTask>();
                //var taskInProgress = session.Query<SparkTask>().Where(t => t.State.Equals(TaskState.InProgress));
                var result = queriedTasks.ToList();
                return result;
            }
        }

        public IEnumerable<Reminder> GetReminderList() 
        {
            using (var session = _docStore.OpenSession())
            {
                var queriedReminders = session.Query<Reminder>().Where(t => t.Source == (ReminderSource.System));
                var result = queriedReminders.ToList();
                return result;
            }
        }

        public SparkTask GetActiveTask()
        {
            using (var session = _docStore.OpenSession())
            {
                var activeTask = session.Query<SparkTask>().Where(t=> t.State == TaskState.InProgress);
                var result = activeTask.ToArray();
                if (result.Count() == 1)
                {
                    return result[0];
                }
                return null;
            }
        }

        public IEnumerable<SparkTask> GetByStartDate(DateTime startDate, DateTime endDate)
        {
            using (var session = _docStore.OpenSession())
            {
                var queriedTasks = session.Query<SparkTask>().Where(t => t.StartDate.Day >= startDate.Day && t.StartDate < endDate);
                var result = queriedTasks.ToList();
                return result;
            }
        }

    }
}
