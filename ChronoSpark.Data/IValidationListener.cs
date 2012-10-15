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
        void Register<T>(Action<T> validate);
        bool BeforeStore(string key, object entityInstance, RavenJObject metadata, RavenJObject original);
        void AfterStore(string key, object entityInstance, RavenJObject metadata);
    }
}
