using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Common
{
    public static class ExtensionMethods
    {
        public static bool IsNullOrEmpty(this String str)
        {
            return String.IsNullOrEmpty(str);
        }

        public static bool IsNotNullOrEmpty(this String str)
        {
            return !String.IsNullOrEmpty(str);
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }


}
