using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client.Document;
using Raven.Client.Connection;
using Raven.Client.Embedded;
using System.Runtime.InteropServices;


namespace ChronoSpark.Data
{
    /*  General Comments
     *  1. Move the IRepository interface to its own file.
     *  2. Watch out the indentation, I have to reformat the document because it was all weird.
     *  3. Watch out for naming conventions:
     *      3.1 Use Pascal Case: DocStore, CleanUp, etc., for methods and properties.
     *      3.2 Use "Cammel Back" notation for variables inside the methods: storedDoc, myTask, etc.
     *  4. Try to use descriptive names, like, if you are retrieving an document from the db, don't call it
     *     "task", that's the most generic name ever, called descriptibly: storedDocument, toDeleteTask, etc.,
     *     the idea is that your code can be EASILY, like a sentence.
     *  5. Once that you implement some of the annotations put here, delete them.
     */
    public class Repository : IRepository, IDisposable
    {
        private DocumentStore DocStore; //Let's follow C#'s convention an Pascal Case all variables
        private bool Disposed = false;

        public bool Initialize()
        {
            DocStore = new EmbeddableDocumentStore()
            {
                ConnectionStringName = "RavenDB"
            }; //http server needed?? No. And you can even get the Management Studio running: http://goo.gl/cEn9g

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
            return true;
        }

        public bool Add<T>(T task)
        {
            using (var Session = DocStore.OpenSession())
            {

                Task newTask = Session.Load<Task>("Task.ID");
                Session.Store(newTask);
                /*  Validations Missing 
                 *  1. You never checked for task being null or empty.
                 *  2. You never checked that task has all the necessary property field out
                 *      2.1 What would happend if task doesn't have a description or a time amount?
                 */

                Session.Store(newTask);
                Session.SaveChanges();
            }
            return true;
        }

        public bool Update<T>(T task)
        {
            using (var Session = DocStore.OpenSession())
            {
                /*  Validation Missing
                 *  1. You are not checking that the storedTask actually exists
                 *  2. You are not checking the that task is not null or empty
                 *  3. You are not checking that task has a valid ID.
                 */

                var storedTask = Session.Load<Task>("Task.ID");
                /*When and how are you putting the values of task into the object that you got from the 
                 * session? Let's use something called ValueInjecter, is a object mapper. http://goo.gl/izdEm
                 * You can install that via Nuget.
                */
                Session.Store(task);
                Session.SaveChanges();
            }

            return true;
        }

        public bool Delete<T>(T task)
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

                var storedTask = Session.Load<Task>("Task.ID");
                Session.Delete(storedTask);
                Session.SaveChanges();

                return true;
            }
        }
    }
}
