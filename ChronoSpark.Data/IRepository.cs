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
        bool Add<T>(T task);
        bool Update<T>(T task);
        bool Delete<T>(T task);

    }
}
