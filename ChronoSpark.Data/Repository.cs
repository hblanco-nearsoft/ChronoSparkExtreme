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
        bool Initialize();
        bool Cleanup();
        bool Add<T>(T task);
        bool Update<T>(T task);
        bool Dcelete<T>(T task);
       
    }

    public class Repository : IRepository
    { 
        private DocumentStore Docstore;

        public bool Initialize() {
            Docstore = new EmbeddableDocumentStore{ConnectionStringName="RavenDB", UseEmbeddedHttpServer=true}; //http server needed??
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
                var task = Session.Load<Task>("Task.ID");
                Session.Store(task);
                Session.SaveChanges();
            
            }

            return true;
        }

        public bool Delete<T>(T Task)
        {
            using (var Session = Docstore.OpenSession())
            {
                var task = Session.Load<Task>("Task.ID");
                Session.Delete(task);
                Session.SaveChanges();
            }
               return true;
        }

        }
    }
