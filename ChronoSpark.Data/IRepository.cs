using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Data
{
    public interface IRepository
    {
        bool Initialize();
        bool CleanUp();
        bool Add<T>(T task) where T : class, ChronoSpark.Data.Entities.IRavenEntity;
        bool Update<T>(T task) where T : class, ChronoSpark.Data.Entities.IRavenEntity;
        bool Delete<T>(T task) where T : class, ChronoSpark.Data.Entities.IRavenEntity;

    }
}
