﻿using Raven.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChronoSpark.Common;
using Raven.Client.Document;
using Raven.Client.Listeners;
using ChronoSpark.Data.Entities;
using Raven.Client.Embedded;


namespace ChronoSpark.Data
{
    public class ChronoDocumentStore : EmbeddableDocumentStore, IDocumentStoreListener
    {
        public ChronoDocumentStore()    
            : base()
        {
            this.Register<Task>(task => { if (task.Description.IsNullOrEmpty()) throw new ArgumentNullException("Description cannot be null"); });
            this.Register<Task>(task => { if (task.Duration <= 0) throw new IndexOutOfRangeException("Duration Should be longer than 0"); });
            this.Register<Reminder>(reminder => { if (reminder.Description.IsNullOrEmpty()) throw new ArgumentNullException("Description cannto be null"); });
            this.Register<Reminder>(reminder => { if (reminder.Interval <= 0) throw new IndexOutOfRangeException("The Inverval must be longer than 0"); });

            RegisterListener(this);
        }

        readonly Dictionary<Type, List<Action<object>>> validations = new Dictionary<Type, List<Action<object>>>();
     
        public void Register<T>(Action<T> validate)
        {
            List<Action<object>> list;
            if(validations.TryGetValue(typeof(T),out list) == false)
                validations[typeof (T)] = list = new List<Action<object>>();
    
           list.Add(o => validate((T) o));
       }
    
       public bool BeforeStore(string key, object entityInstance, RavenJObject metadata, RavenJObject original)
       {
           List<Action<object>> list;
           if (validations.TryGetValue(entityInstance.GetType(), out list))
           {
               foreach (var validation in list)
               {
                   validation(entityInstance);
               }
           }
           return false;
       }
    
       public void AfterStore(string key, object entityInstance, RavenJObject metadata)
       {
       }
   }
}
