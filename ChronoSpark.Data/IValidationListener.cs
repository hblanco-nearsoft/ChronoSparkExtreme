using Raven.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Data
{
    public interface IValidationListener
    {
        public void Register<T>(Action<T> validate);
        public bool BeforeStore(string key, object entityInstance, RavenJObject metadata, RavenJObject original);
        public void AfterStore(string key, object entityInstance, RavenJObject metadata);
    }
}
