using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omu.ValueInjecter;

namespace ChronoSpark.Common
{
    public class InjectNotNull : ConventionInjection
    {

        protected override bool Match(ConventionInfo c)
        {
            if (c.SourceProp.Name != c.TargetProp.Name) { return false; }
            if (c.SourceProp.Value == null) { return false; }
            if (c.SourceProp.Type == typeof(int))
            {
                if(c.SourceProp.Value.Equals(0)){return false;}
                return true;
            }
            if (c.SourceProp.Type == typeof(DateTime)) 
            {
                if (c.SourceProp.Value.Equals(default(DateTime))) { return false; }
                return true;
            }
            //return c.SourceProp.Value != null;
            return true;
        }

    }
}
