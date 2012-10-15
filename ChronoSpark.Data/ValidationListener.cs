﻿using Raven.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Data
{
     public class ValidationListener : IValidationListener
    {
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
