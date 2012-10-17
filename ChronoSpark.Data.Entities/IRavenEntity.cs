using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Data
{
    public interface IRavenEntity
    {
        String Id { get; set; }
        String LoadString();
        bool Validate();
        bool ValidateToAdd();
    }
}
