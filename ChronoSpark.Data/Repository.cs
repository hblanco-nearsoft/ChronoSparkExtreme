using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Document;
using Raven.Client.Connection;
using Raven.Client.Embedded;


namespace ChronoSpark.Data
{
    public interface IRepository
    {
        bool Add<T>(T task);
    }

    public class Repository
    { 
        private DocumentStore Docstore;

        public bool Initialize() {
            Docstore = new EmbeddableDocumentStore{ConnectionStringName="RavenDB", UseEmbeddedHttpServer=true}; // connection string where art thou?
        Docstore.Initialize();
        return true;
        }

        public bool Cleanup() {
            Docstore.Dispose();
            return true;
        }
        
        public bool Add<T>(T Task)
        {
                using (var Session = Docstore.OpenSession())
                {
                    Session.Store(Task);
                    Session.SaveChanges();
                }
            return true;
        }

        public bool Update<T>(T Task)
        {
            using (var Session = Docstore.OpenSession())
            {
                String TaskID;
                TaskID = "Task";  //get the id
                var task = Session.Load<Task>(TaskID);
                Session.Delete(Task);
                Session.SaveChanges();
            
            }

            return true;
        }

        public bool Delete<T>(T Task)
        {
            using (var Session = Docstore.OpenSession())
            {
                String TaskID;
                TaskID = "Task";
                var task = Session.Load<Task>(TaskID);
            }
               return true;
        }

        }
    }
