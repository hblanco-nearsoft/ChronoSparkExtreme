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

namespace ChronoSpark.Data
{
    /*  General Comments
     *     Try to use descriptive names, like, if you are retrieving an document from the db, don't call it
     *     "task", that's the most generic name ever, called descriptibly: storedDocument, toDeleteTask, etc.,
     *     the idea is that your code can be EASILY, like a sentence.
     *     
     * Oct. 15. Good Work! We are almost there with the data layer, need some unit testing though.
     */
    public class Repository : IRepository, IDisposable 
    {
        private DocumentStore DocStore; //Let's follow C#'s convention an Pascal Case all variables
        private bool Disposed = false;

        public bool Initialize()
        {
            DocStore = new EmbeddableDocumentStore()
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

        public bool CleanUp()
        {
            return true;
        }

        public virtual bool Dispose(bool disposing)
        {
              /*  Validation Missing:
                    1. What if we call Cleanup and DocStore is null?
             */

            /* Design Note: This cleanup would be better if we doing in the Dispose method of
             * IDisposable interface, make this class implement that.
                http://goo.gl/GwXQQ
             */
            if (!Disposed)
            {
                if (disposing)
                {
                    if (DocStore != null)
                    {
                         DocStore.Dispose();
                    }
                }
                Disposed = true;
            }
            return true;
        }

        public bool Dispose()
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

        public bool Add<T>(T task) where T : class, IRavenEntity
        {
            using (var Session = DocStore.OpenSession())
            {
                /*  Validations Missing 
                 *  1. You never checked for task being null or empty.
                 *  2. You never checked that task has all the necessary property field out
                 *      2.1 What would happend if task doesn't have a description or a time amount?
                 */
                if (task.Validate())
                {
                    Session.Store(task);
                    Session.SaveChanges();
                    return true;
                }
                return false;
            }  
        }

        //This Method is kind of cool! good work.
        public bool Update<T>(T task) where T : class, IRavenEntity
        {
            using (var Session = DocStore.OpenSession())
            {
                /*  Validation Missing
                 *  1. You are not checking that the storedTask actually exists
                 *  2. You are not checking the that task is not null or empty
                 *  3. You are not checking that task has a valid ID.
                 */
                var doc = Session.Load<T>(task.LoadString());
                doc.InjectFrom(task);
                if (doc.Validate())
                {
                    Session.Store(doc);
                    Session.SaveChanges();
                    return true;
                }
                return false;
            }   
        }

        public bool Delete<T>(T task) where T : class, IRavenEntity
        {
            using (var Session = DocStore.OpenSession())
            {
                /* Validations Missing  
                 *  1. Check that the task is not null or empty
                 *  2. Check that task has a valid id
                 *  3. Check that the id that you want to delete, actually exists and that you can delete.
                 *  4. I don't know right now if we are going to have a special rule for deletion, but if
                 *     we decide so, we would need to also delete it.
                 */


                /*  What are you going to validate here?
                 *  Remember that we only need to have a VALID object ID to be able to delete it,
                 *  and you are trying to load if BEFORE knowing if we have it.
                 */
                var doc = Session.Load<T>(task.LoadString());
                
                if (doc.Validate()) 
                {

                    Session.Delete(doc);
                    Session.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
